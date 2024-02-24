using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Model
{
    internal class OperationEquipment : Equipment
    {
        public OperationEquipment(string name)
        {
            Category = EquipmentCategory.OperationEquipment;
            IsDynamic = true;
            Name = name;
        }
    }

    internal class ExaminationEquipment : Equipment
    {
        public ExaminationEquipment()
        {
            Category = EquipmentCategory.ExaminationEquipment;
            IsDynamic = true;
        }
    }

    internal class Stationery : Equipment
    {
        public Stationery()
        {
            Category = EquipmentCategory.Stationery;
            IsDynamic = true;
        }
    }

    internal class OperationFurniture : Equipment
    {
        public OperationFurniture()
        {
            Category = EquipmentCategory.OperationFurniture;
            IsDynamic = false;
        }
    }

    internal class RoomFurniture : Equipment
    {
        public RoomFurniture()
        {
            Category = EquipmentCategory.RoomFurniture;
            IsDynamic = false;
        }
    }
}
