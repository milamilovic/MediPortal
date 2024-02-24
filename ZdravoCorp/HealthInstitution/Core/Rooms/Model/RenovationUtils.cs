using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Configuration;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{
    public class RenovationUtils
    {
        public static bool CheckDates(DatePicker startDatePicker, DatePicker endDatePicker,
            ref DateTime startDate, ref DateTime endDate)
        {
            try
            {
                startDate = startDatePicker.SelectedDate.Value.Date;
                endDate = endDatePicker.SelectedDate.Value.Date;

                if (startDate >= endDate || startDate <= DateTime.Now)
                {
                    MessageBox.Show("Please select valid dates!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select valid dates!");
                return false;
            }

            return true;
        }


        public static int GetSelectedRoom(ComboBox roomCombo)
        {
            int roomId;
            try
            {
                roomId = (int)roomCombo.SelectedValue;
                return roomId;
            }
            catch
            {
                MessageBox.Show("Please select the room!");
                return -1;
            }
        }

        public static bool IsChecked(CheckBox keepEquipment)
        {
            return keepEquipment.IsChecked == true;
        }

        public static Room.Type GetSelectedRoomType(ComboBox roomTypePicker)
        {
            Room.Type roomType;
            try
            {
                roomType = RoomSevice.UndoFormat((string)roomTypePicker.SelectedValue);
                if (roomType == Room.Type.Warehouse)
                {
                    MessageBox.Show("Please select room type!");
                }
                return roomType;
            }
            catch
            {
                MessageBox.Show("Please select room type!");
                // Default to Warehouse if no valid room type is selected
                return Room.Type.Warehouse;
            }
        }

        public static void SimpleRenovation(ComboBox roomIdPicker, DatePicker startDatePicker,
            DatePicker endDatePicker)
        {
            int roomId = GetSelectedRoom(roomIdPicker);
            if (roomId == -1) return;

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            bool validDates = CheckDates(startDatePicker, endDatePicker, ref startDate,
                ref endDate);

            if (!validDates) return;

            SimpleRoomRenovation.SimpleRenovation(roomId, startDate, endDate, false);
        }

        public static void SplittingRoom(ComboBox roomIdPicker, DatePicker startDatePicker,
            DatePicker endDatePicker, CheckBox keepEquipment, bool forCli)
        {
            int roomId = GetSelectedRoom(roomIdPicker);
            if (roomId == -1) return;

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            bool validDates = CheckDates(startDatePicker, endDatePicker, ref startDate,
                ref endDate);

            if (!validDates) return;

            bool keepEquipmentInRoom = IsChecked(keepEquipment);
            SplitRoom(roomId, startDate, endDate, keepEquipmentInRoom, forCli);
        }

        private static void SplitRoom(int roomId, DateTime startDate, DateTime endDate, bool keepEquipmentInRoom, bool forCli)
        {
            RoomSplittingService service = new RoomSplittingService();
            service.SplitRoom(roomId, startDate, endDate, keepEquipmentInRoom, forCli);
        }


        public static void CombiningRooms(ComboBox firstRoomIdPicker, ComboBox secondRoomIdPicker, DatePicker startDatePicker, DatePicker endDatePicker, CheckBox keepEquipment, ComboBox roomTypePicker)
        {
            int firstRoomId = GetSelectedRoom(firstRoomIdPicker);
            int secondRoomId = GetSelectedRoom(secondRoomIdPicker);
            if (firstRoomId == -1 || secondRoomId == -1)
            {
                return;
            }


            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool validDates = CheckDates(startDatePicker, endDatePicker, ref startDate,
                ref endDate);

            if (!validDates) return;

            bool keepEquipmentInRoom = IsChecked(keepEquipment);
            Room.Type roomType = GetSelectedRoomType(roomTypePicker);
            if (roomType == Room.Type.Warehouse) return;

            CombineRooms(firstRoomId, secondRoomId, startDate, endDate, roomType, keepEquipmentInRoom);
        }

        private static void CombineRooms(int firstRoomId, int secondRoomId, DateTime startDate,
            DateTime endDate, Room.Type roomType, bool keepEquipmentInRoom)
        {
            RoomCombiningService service = new RoomCombiningService();
            service.RoomCombining(firstRoomId, secondRoomId, startDate,
                endDate, roomType, keepEquipmentInRoom, false);
        }
    }
}
