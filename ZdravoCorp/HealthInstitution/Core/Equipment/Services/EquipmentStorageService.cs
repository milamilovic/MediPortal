using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Net;
using static ZdravoCorp.HealthInstitution.Core.Equipment.Model.Equipment;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Services
{
    public enum QuantityRange { None, OneToTen, MoreThanTen, NoneToTen, NoneOrTenPlus, OneToInfinity }

    internal class EquipmentStorageService
    {
        public static bool IsChanged = false;
        private static void Serialize(ref List<EquipmentStorageItem> items, string fileName)
        {
            dynamic text = JsonConvert.SerializeObject(items, Formatting.Indented);
            while (true)
            {
                try
                {
                    File.WriteAllText(fileName, text);
                    return;
                }
                catch (Exception ex) { }
            }
        }

        private static List<EquipmentStorageItem> Deserialize(string fileName)
        {
            string jsontext;
            while (true)
            {
                try
                {
                    jsontext = File.ReadAllText(fileName);
                    break;
                }
                catch (Exception ex) { }
            }
            List<EquipmentStorageItem> items = JsonConvert.DeserializeObject<List<EquipmentStorageItem>>(jsontext)!; ;
            return items;
        }

        public static List<EquipmentStorageItem> GetAllItems(string fileName)
        {
            return Deserialize(fileName);
        }

        public static List<EquipmentStorageItem> GetAllItemsForRoom(string fileName, int roomId)
        {
            List<EquipmentStorageItem> items = Deserialize(fileName);
            List<EquipmentStorageItem> adrequateItems = new List<EquipmentStorageItem>();
            foreach (var item in items)
            {
                if (item.ContainingRoomId == roomId)
                {
                    adrequateItems.Add(item);
                }
            }
            return adrequateItems;
        }
        public static string GetEquipmentCategory(EquipmentStorageItem item)
        {
            return EquipmentService.GetEquipmentCategoryByName(item.StoredEquipmentName).ToString();
        }
        public static bool IsEquipmentStatic(EquipmentStorageItem item)
        {
            if (EquipmentService.IsEquipmentDinamicByName(item.StoredEquipmentName))
            {
                return false;
            }
            return true;
        }

        public static string GetEquipmentType(EquipmentStorageItem item)
        {
            if (EquipmentService.IsEquipmentDinamicByName(item.StoredEquipmentName))
            {
                return "dynamic";
            }
            return "static";
        }

        public static Room.Type GetRoomType(EquipmentStorageItem item)
        {
            return RoomSevice.GetRoomTypeById(item.ContainingRoomId, false);
        }

        public static EquipmentStorageItem FindItem(string name, int roomId, ref List<EquipmentStorageItem> allItems)
        {
            name = EquipmentService.UndoFormat(name);
            foreach (EquipmentStorageItem item in allItems)
            {
                if (item.StoredEquipmentName == name && item.ContainingRoomId == roomId)
                {
                    return item;
                }
            }
            return null;
        }

        public static void ChangeItemQuantityInRoom(string itemName, int roomId, int quantityChange)
        {
            itemName = EquipmentService.UndoFormat(itemName);
            string fileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";
            List<EquipmentStorageItem> allItems = GetAllItems(fileName);
            foreach (EquipmentStorageItem item in allItems)
            {
                if (item.StoredEquipmentName == itemName && item.ContainingRoomId == roomId)
                {
                    item.Quantity += quantityChange;
                    Serialize(ref allItems, fileName);
                    IsChanged = true;
                    return;
                }
            }
        }

        internal static void RemoveRoom(int roomId)
        {
            string fileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";
            List<EquipmentStorageItem> allItems = GetAllItems(fileName);
            List<EquipmentStorageItem> updatedItems = new List<EquipmentStorageItem>();
            foreach (EquipmentStorageItem item in allItems)
            {
                if (item.ContainingRoomId != roomId)
                {
                    updatedItems.Add(item);
                }
            }
            Serialize(ref updatedItems, fileName);
        }

        internal static void AddRoom(int id, Room.Type roomType, bool forCli)
        {
            string fileName = "../../../Data/EquipmentStorage/EquipmentStorage.json";
            if(forCli) fileName = "../../../../ZdravoCorp/Data/EquipmentStorage/EquipmentStorage.json";
            List<EquipmentStorageItem> allItems = GetAllItems(fileName);
            List<string> allEquipmentNames = EquipmentService.GetAllEquipmentTypes();
            foreach (string item in allEquipmentNames)
            {
                string itemName = EquipmentService.UndoFormat(item);
                List<Room.Type> validRoomTypes = EquipmentService.GetValidRoomTypesForEquipment(itemName);
                if (validRoomTypes.Contains(roomType))
                {
                    EquipmentStorageItem storageItem = new EquipmentStorageItem(itemName, id);
                    allItems.Add(storageItem);
                }
            }
            Serialize(ref allItems, fileName);
        }
    }
}