using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Services
{
    //a class that links equipment with room
    //contains information about quontity of said equipment in a specific room
    public class EquipmentStorageItem
    {
        public string StoredEquipmentName { get; set; }

        public int ContainingRoomId { get; set; }

        public int Quantity { get; set; }
        public EquipmentStorageItem(string equipmentName, int roomId, int quantity = 0)
        {
            StoredEquipmentName = equipmentName;
            ContainingRoomId = roomId;
            Quantity = quantity;
        }

    }
}
