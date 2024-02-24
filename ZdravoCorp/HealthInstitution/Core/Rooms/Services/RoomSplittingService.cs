using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public class RoomSplittingService : IComplexRenovationService
    {
        public void SplitRoom(int roomId, DateTime startDate, DateTime endDate,
            bool returnEquipmentToRoom, bool forCli)
        {
            bool valid = ValidateRooms(roomId, 0, startDate, endDate, forCli);
            if (!valid) { return; }

            ScheduleEquipmentMoving(roomId, 0, startDate, endDate, returnEquipmentToRoom, forCli);

            Schedule(roomId, 0, startDate, endDate, forCli);
            if (!forCli)
            {
                MessageBox.Show("Renovation is scheduled!");
            }
            return;
        }

        public void Schedule(int firstRoomId, int secondRoomId, DateTime startDate,
            DateTime endDate, bool forCli)
        {
            int roomId = firstRoomId;
            RoomRenovationService.MarkDatesAsOccupied(roomId, startDate, endDate, forCli);

            Room.Type roomType = RoomSevice.GetRoomTypeById(roomId, forCli);
            int newId = RoomSevice.GetNewId(forCli);
            Room newRoom = new Room(roomType, newId);
            TimeSlot renovationTime = RoomRenovationService.CreateRenovationTimeSlot(startDate, endDate);

            AddRenovationSchedule(roomId, secondRoomId, newRoom, renovationTime, forCli);
        }

        public bool ValidateRooms(int firstRoomId, int secondRoomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            int roomId = firstRoomId;
            const int WAREHOUSE_ID = 11;

            if (!IsRoomAvailableForRenovation(roomId, startDate, endDate, forCli))
                return false;

            if (RoomRenovationService.IsWarehouseInvolvedInRenovation(roomId, WAREHOUSE_ID))
                return false;
            return true;
        }

        public void AddRenovationSchedule(int firstRoomId, int secondRoomId,
            Room newRoom, TimeSlot renovationTime, bool forCli)
        {
            RoomSplittingSchedule renovation = new RoomSplittingSchedule(firstRoomId, newRoom, renovationTime);
            RoomSplittingSchedule.AddRenovation(renovation, forCli);
        }

        public void ScheduleEquipmentMoving(int firstRoomId, int secondRoomId,
            DateTime startDate, DateTime endDate, bool returnEquipment, bool forCli)
        {
            int roomId = firstRoomId;
            string equipmentStorageFileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";
            if(forCli) equipmentStorageFileName = "../../../../ZdravoCorp/Data/EquipmentStorage/EquipmentStorage.json";
            const int WAREHOUSE_ID = 11;

            RoomRenovationService.MoveEquipmentToWarehouse(roomId, WAREHOUSE_ID, startDate,
                equipmentStorageFileName, forCli);

            if (returnEquipment)
            {
                RoomRenovationService.MoveEquipmentFromWarehouse(roomId, WAREHOUSE_ID,
                    endDate, equipmentStorageFileName, forCli);
            }
        }

        private static bool IsRoomAvailableForRenovation(int roomId,
            DateTime startDate, DateTime endDate, bool forCli)
        {
            bool isFirstRoomFree = RoomRenovationService.IsRoomFreeOnDateRange(roomId,
                startDate, endDate, forCli);

            if (!isFirstRoomFree && !forCli)
            {
                MessageBox.Show("Room is not free in these dates, please try other dates!");
                return false;
            }

            return true;
        }
    }
}
