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
    public class RoomCombiningService : IComplexRenovationService
    {
        public void RoomCombining(int firstRoomId, int secondRoomId, DateTime startDate,
            DateTime endDate, Room.Type resultingRoomType, bool returnEquipmentToRoom, bool forCli)
        {
            bool valid = ValidateRooms(firstRoomId, secondRoomId, startDate, endDate, forCli);
            if (!valid) { return; }

            ScheduleEquipmentMoving(firstRoomId, secondRoomId, startDate, endDate, returnEquipmentToRoom, forCli);

            Schedule(firstRoomId, secondRoomId, startDate, endDate, resultingRoomType, forCli);

            if (!forCli)
            {
                MessageBox.Show("Renovation is scheduled!");
            }
        }

        public void Schedule(int firstRoomId, int secondRoomId, DateTime startDate,
            DateTime endDate, Room.Type resultingRoomType, bool forCli)
        {
            RoomRenovationService.MarkDatesAsOccupied(firstRoomId, startDate, endDate, forCli);
            RoomRenovationService.MarkDatesAsOccupied(secondRoomId, startDate, endDate, forCli);

            Room resultingRoom = new Room(resultingRoomType, firstRoomId);
            TimeSlot renovationTime = RoomRenovationService.CreateRenovationTimeSlot(startDate, endDate);

            AddRenovationSchedule(firstRoomId, secondRoomId, resultingRoom, renovationTime, forCli);
        }

        public bool ValidateRooms(int firstRoomId, int secondRoomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            const int WAREHOUSE_ID = 11;

            if (!AreRoomsAvailableForRenovation(firstRoomId, secondRoomId, startDate, endDate, forCli))
                return false;

            if (RoomRenovationService.IsWarehouseInvolvedInRenovation(firstRoomId, secondRoomId, WAREHOUSE_ID))
                return false;
            return true;
        }

        public void ScheduleEquipmentMoving(int firstRoomId, int secondRoomId,
            DateTime startDate, DateTime endDate, bool returnEquipment, bool forCli)
        {
            string equipmentStorageFileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";
            if(forCli) equipmentStorageFileName = "../../../../ZdravoCorp/Data/EquipmentStorage/EquipmentStorage.json";
            const int WAREHOUSE_ID = 11;

            RoomRenovationService.MoveEquipmentToWarehouse(firstRoomId, secondRoomId, WAREHOUSE_ID, startDate,
                equipmentStorageFileName, forCli);

            if (returnEquipment)
            {
                RoomRenovationService.MoveEquipmentFromWarehouse(firstRoomId, secondRoomId, WAREHOUSE_ID,
                    endDate, equipmentStorageFileName, forCli);
            }
        }

        private bool AreRoomsAvailableForRenovation(int firstRoomId, int secondRoomId,
            DateTime startDate, DateTime endDate, bool forCli)
        {
            bool isFirstRoomFree = RoomRenovationService.IsRoomFreeOnDateRange(firstRoomId,
                startDate, endDate, forCli);
            bool isSecondRoomFree = RoomRenovationService.IsRoomFreeOnDateRange(secondRoomId,
                startDate, endDate, forCli);

            if (!isFirstRoomFree || !isSecondRoomFree)
            {
                if (!forCli)
                {
                    MessageBox.Show("Room is not free in these dates, please try other dates!");
                }
                return false;
            }

            return true;
        }

        private void AddRenovationSchedule(int firstRoomId, int secondRoomId,
            Room resultingRoom, TimeSlot renovationTime, bool forCli)
        {
            RoomCombiningSchedule renovation = new RoomCombiningSchedule(firstRoomId, secondRoomId,
                resultingRoom, renovationTime);
            RoomCombiningSchedule.AddRenovation(renovation, forCli);
        }

    }
}
