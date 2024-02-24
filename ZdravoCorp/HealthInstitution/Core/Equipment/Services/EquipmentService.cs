using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Equipment.Model;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using static ZdravoCorp.HealthInstitution.Core.Equipment.Model.Equipment;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Services
{
    public class EquipmentService
    {
        public static string[] OperationEquipmentOptions = {
            "Scissors",
            "Mask",
            "OperationTray",
            "Scalpel",
            "Tweezers",
            "RubberGlove",
            "OxygenMask"
        };

        public static string[] ExaminationEquipmentOptions = {
            "Stethoscope",
            "Plasters",
            "Thermometer",
            "Bandage",
            "Injection",
            "Gauze",
            "Syringe",
            "TongueDepressor"
        };

        public static string[] StationeryOptions = {
            "Pencil",
            "Pen",
            "Notepad",
            "PatientDatailForm",
            "PrescriptionPad"
        };

        public static string[] OperationFurnitureOptions = {
            "SurgicalTable",
            "AnesthesiaMachine",
            "PatientMonitor",
            "Trolley"
        };

        public static string[] RoomFurnitureOptions = {
            "Wheelchair",
            "HospitalBed",
            "BedsideScreen",
            "Locker",
            "Table",
            "Chair"
        };

        public static string Format(string name)
        {
            switch (name)
            {
                case "HospitalBed":
                    return "Hospital bed";
                case "BedsideScreen":
                    return "Bedside screen";
                case "PatientMonitor":
                    return "Patient monitor";
                case "AnesthesiaMachine":
                    return "Anesthesia machine";
                case "SurgicalTable":
                    return "Surgical table";
                case "PrescriptionPad":
                    return "Prescription pad";
                case "PatientDatailForm":
                    return "Patient detail form";
                case "TongueDepressor":
                    return "Tongue depressor";
                case "OxygenMask":
                    return "Oxygen mask";
                case "RubberGlove":
                    return "Rubber glove";
                case "OperationTray":
                    return "Operation tray";
                default:
                    return name;

            }
        }
        public static string UndoFormat(string name)
        {
            switch (name)
            {
                case "Hospital bed":
                    return "HospitalBed";
                case "Bedside screen":
                    return "BedsideScreen";
                case "Patient monitor":
                    return "PatientMonitor";
                case "Anesthesia machine":
                    return "AnesthesiaMachine";
                case "Surgical table":
                    return "SurgicalTable";
                case "Prescription pad":
                    return "PrescriptionPad";
                case "Patient detail form":
                    return "PatientDatailForm";
                case "Tongue depressor":
                    return "TongueDepressor";
                case "Oxygen mask":
                    return "OxygenMask";
                case "Rubber glove":
                    return "RubberGlove";
                case "Operation tray":
                    return "OperationTray";
                default:
                    return name;

            }
        }
        public static EquipmentCategory GetEquipmentCategoryByName(string name)
        {
            name = UndoFormat(name);
            if (OperationEquipmentOptions.Contains(name))
            {
                return EquipmentCategory.OperationEquipment;
            }
            else if (ExaminationEquipmentOptions.Contains(name))
            {
                return EquipmentCategory.ExaminationEquipment;
            }
            else if (StationeryOptions.Contains(name))
            {
                return EquipmentCategory.Stationery;
            }
            else if (OperationFurnitureOptions.Contains(name))
            {
                return EquipmentCategory.OperationFurniture;
            }
            else
            {
                return EquipmentCategory.RoomFurniture;
            }
        }

        public static bool IsEquipmentDinamicByName(string name)
        {
            name = UndoFormat(name);
            if (OperationEquipmentOptions.Contains(name))
            {
                return true;
            }
            else if (ExaminationEquipmentOptions.Contains(name))
            {
                return true;
            }
            else if (StationeryOptions.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Room.Type> GetValidRoomTypesForEquipment(string equipmentName)
        {
            equipmentName = UndoFormat(equipmentName);
            EquipmentCategory equipmentCategory = GetEquipmentCategoryByName(equipmentName);
            var validRooms = new List<Room.Type>();
            //anything can be in the warehouse
            validRooms.Add(Room.Type.Warehouse);
            switch (equipmentCategory)
            {
                case EquipmentCategory.OperationEquipment:
                    validRooms.Add(Room.Type.OperatingRoom);
                    break;
                case EquipmentCategory.ExaminationEquipment:
                    validRooms.Add(Room.Type.ConsultingRoom);
                    break;
                case EquipmentCategory.Stationery:
                    validRooms.Add(Room.Type.OperatingRoom);
                    validRooms.Add(Room.Type.PatientCareRoom);
                    validRooms.Add(Room.Type.ConsultingRoom);
                    validRooms.Add(Room.Type.WaitingRoom);
                    break;
                case EquipmentCategory.OperationFurniture:
                    validRooms.Add(Room.Type.OperatingRoom);
                    break;
                case EquipmentCategory.RoomFurniture:
                    validRooms.Add(Room.Type.OperatingRoom);
                    validRooms.Add(Room.Type.ConsultingRoom);
                    validRooms.Add(Room.Type.PatientCareRoom);
                    validRooms.Add(Room.Type.WaitingRoom);
                    break;
            }
            return validRooms;
        }

        public static List<string> GetAllEquipmentTypes()
        {
            List<string> allEquipmentTypes = new List<string>();
            foreach (string equipment in OperationEquipmentOptions)
            {
                allEquipmentTypes.Add(Format(equipment));
            }
            foreach (string equipment in OperationFurnitureOptions)
            {
                allEquipmentTypes.Add(Format(equipment));
            }
            foreach (string equipment in ExaminationEquipmentOptions)
            {
                allEquipmentTypes.Add(Format(equipment));
            }
            foreach (string equipment in StationeryOptions)
            {
                allEquipmentTypes.Add(Format(equipment));
            }
            foreach (string equipment in RoomFurnitureOptions)
            {
                allEquipmentTypes.Add(Format(equipment));
            }
            return allEquipmentTypes;
        }
    }
}
