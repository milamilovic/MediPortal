using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.Equipment.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public static class RoomRenovationService
    {
        public static bool IsRoomFree(int roomId, TimeSlot time, bool forCli)
        {
            List<RoomSchedule> schedules = RoomSchedule.GetAppointments(forCli);
            foreach (RoomSchedule appointment in schedules)
            {
                if (appointment.roomId != roomId)
                {
                    continue;       //if it's not the same room it's not relevant
                }
                //if appointsment overlap room is not free
                if (RoomSchedule.CheckOverlap(appointment, time))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsRoomFreeOnDateRange(int roomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            List<RoomSchedule> schedules = RoomSchedule.GetAppointments(forCli);
            DateTime date = startDate;
            foreach (RoomSchedule appointment in schedules)
            {
                while (date != endDate)
                {
                    string justDate = GetFormattedDate(startDate);
                    string time = GetFormattedTime(startDate);
                    TimeSlot timeSlot = CreateTimeSlot(justDate, time);
                    if (!IsRoomFree(roomId, timeSlot, forCli)) return false;
                    date = date.AddDays(1);
                }
            }
            return true;
        }

        public static void MarkDatesAsOccupied(int roomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            while (startDate != endDate)
            {
                string justDate = GetFormattedDate(startDate);
                string time = GetFormattedTime(startDate);
                TimeSlot timeSlot = CreateTimeSlot(justDate, time);
                //if appointment overlaps, room is not free
                int appointmentId = GenerateAppointmentId();
                RoomSchedule occupiedTime = CreateOccupiedTime(roomId, appointmentId, timeSlot);
                RoomSchedule.AddSimpleAppointment(occupiedTime, forCli);
                startDate = startDate.AddDays(1);
            }
        }

        private static string GetFormattedDate(DateTime date)
        {
            return date.Date.ToString("dd.MM.yyyy.");
        }

        private static string GetFormattedTime(DateTime time)
        {
            return time.TimeOfDay.ToString();
        }

        private static TimeSlot CreateTimeSlot(string date, string time)
        {
            return new TimeSlot(date, time, "01:00:00");
        }

        private static int GenerateAppointmentId()
        {
            Random random = new Random();
            return random.Next(10000, 999999);
        }

        private static RoomSchedule CreateOccupiedTime(int roomId, int appointmentId, TimeSlot timeSlot)
        {
            return new RoomSchedule(roomId, appointmentId, timeSlot);
        }


        public static void MarkDatesAsOccupied(int firstRoomId, int secondRoomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            MarkDatesAsOccupied(firstRoomId, startDate, endDate, forCli);
            MarkDatesAsOccupied(secondRoomId, startDate, endDate, forCli);
        }

        public static bool IsWarehouseInvolvedInRenovation(int firstRoomId, int secondRoomId, int warehouseId)
        {
            if (firstRoomId == warehouseId || secondRoomId == warehouseId)
            {
                MessageBox.Show("You cannot renovate the warehouse!");
                return true;
            }

            return false;
        }

        public static bool IsWarehouseInvolvedInRenovation(int roomId, int warehouseId)
        {
            if (roomId == warehouseId)
            {
                MessageBox.Show("You cannot renovate the warehouse!");
                return true;
            }

            return false;
        }

        public static void MoveEquipmentToWarehouse(int roomId, int warehouseId, DateTime startDate,
            string fileName, bool forCli)
        {
            List<EquipmentStorageItem> items = EquipmentStorageService.GetAllItemsForRoom(fileName, roomId);

            foreach (EquipmentStorageItem item in items)
            {
                EquipmentMovingService.RequestMovingWithSchedulingDynamic(item.StoredEquipmentName,
                    roomId, warehouseId, item.Quantity, startDate, forCli);
            }
        }

        public static void MoveEquipmentToWarehouse(int firstRoomId, int secondRoomId, int warehouseId, DateTime startDate,
            string fileName, bool forCli)
        {
            MoveEquipmentToWarehouse(firstRoomId, warehouseId, startDate, fileName, forCli);
            MoveEquipmentToWarehouse(secondRoomId, warehouseId, startDate, fileName, forCli);
        }

        public static void MoveEquipmentFromWarehouse(int roomId, int warehouseId, DateTime endDate,
            string fileName, bool forCli)
        {
            MoveEquipmentToWarehouse(warehouseId, roomId, endDate, fileName, forCli);
        }

        public static void MoveEquipmentFromWarehouse(int firstRoomId, int secondRoomId, int warehouseId, DateTime endDate,
            string fileName, bool forCli)
        {
            MoveEquipmentToWarehouse(warehouseId, firstRoomId, endDate, fileName, forCli);
            MoveEquipmentToWarehouse(warehouseId, secondRoomId, endDate, fileName, forCli);
        }

        public static TimeSlot CreateRenovationTimeSlot(DateTime startDate, DateTime endDate)
        {
            string startDateString = GetFormattedDate(startDate);
            string time = GetFormattedTime(startDate);
            TimeSpan duration = endDate - startDate;
            string durationString = duration.ToString("dd':'hh':'mm");
            return new TimeSlot(startDateString, time, durationString);
        }

        public static bool IsRoomAvailableForRenovation(int roomId,
           DateTime startDate, DateTime endDate, bool forCli)
        {
            bool isFirstRoomFree = IsRoomFreeOnDateRange(roomId,
                startDate, endDate.Date, forCli);

            if (!isFirstRoomFree)
            {
                MessageBox.Show("Room is not free in these dates, please try other dates!");
                return false;
            }

            return true;
        }
    }
}
