using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public class SimpleRoomRenovation
    {
        public static void SimpleRenovation(int roomId, DateTime startDate, DateTime endDate, bool forCli)
        {

            RoomSplittingService service = new RoomSplittingService();
            bool valid = service.ValidateRooms(roomId, 0, startDate, endDate, false);
            if (!valid) { return; }

            ScheduleEquipmentMoving(roomId, startDate, endDate, forCli);

            RoomRenovationService.MarkDatesAsOccupied(roomId, startDate, endDate, forCli);
            if (!forCli)
            {
                MessageBox.Show("Renovation is scheduled!");
            }
            return;
        }

        private static void ScheduleEquipmentMoving(int roomId, DateTime startDate, DateTime endDate, bool forCli)
        {
            const int WAREHOUSE_ID = 11;
            string equipmentStorageFileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";

            RoomRenovationService.MoveEquipmentToWarehouse(roomId, WAREHOUSE_ID, startDate,
                equipmentStorageFileName, forCli);

            RoomRenovationService.MoveEquipmentFromWarehouse(roomId, WAREHOUSE_ID,
                endDate, equipmentStorageFileName, forCli);
        }
    }
}
