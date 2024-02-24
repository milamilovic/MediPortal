using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Model
{
    public class Equipment
    {
        public enum EquipmentCategory
        {
            OperationEquipment,
            ExaminationEquipment,
            Stationery,
            OperationFurniture,
            RoomFurniture
        }
        public EquipmentCategory Category { get; set; }

        public bool IsDynamic { get; set; }

        public string Name { get; set; } = "";

    }
}
