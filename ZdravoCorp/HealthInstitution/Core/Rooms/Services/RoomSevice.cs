using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public class RoomSevice
    {
        public static void Serialize(List<Room> items, string fileName)
        {
            dynamic text = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        public static List<Room> Deserialize(string fileName)
        {
            var jsontext = File.ReadAllText(fileName);
            List<Room> items = JsonConvert.DeserializeObject<List<Room>>(jsontext)!; ;
            return items;
        }

        public static Room.Type GetRoomTypeById(int id, bool forCli)
        {
            string fileName = "../../../Data/Rooms/Rooms.json";
            if (forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/Rooms.json"; }
            List<Room> rooms = Deserialize(fileName);
            foreach (Room room in rooms)
            {
                if (room.Id == id)
                {
                    return room.RoomType;
                }
            }
            return Room.Type.WaitingRoom;
        }

        public static string Format(Room.Type type)
        {
            switch (type)
            {
                case Room.Type.WaitingRoom:
                    return "Waiting Room";
                case Room.Type.ConsultingRoom:
                    return "Consulting Room";
                case Room.Type.OperatingRoom:
                    return "Operating Room";
                case Room.Type.PatientCareRoom:
                    return "Patient Care Room";
                default:
                    return "Warehouse";
            }
        }

        public static Room.Type UndoFormat(string type)
        {
            switch (type)
            {
                case "Waiting Room":
                    return Room.Type.WaitingRoom;
                case "Consulting Room":
                    return Room.Type.ConsultingRoom;
                case "Operating Room":
                    return Room.Type.OperatingRoom;
                case "Patient Care Room":
                    return Room.Type.PatientCareRoom;
                default:
                    return Room.Type.Warehouse;
            }
        }

        public static List<int> GetAllRoomIds(bool forCli)
        {
            string fileName = "../../../Data/Rooms/Rooms.json";
            if (forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/Rooms.json"; }
            List<Room> rooms = Deserialize(fileName);
            List<int> roomIds = new List<int>();
            foreach (Room room in rooms)
            {
                roomIds.Add(room.Id);
            }
            return roomIds;
        }

        public static List<Room.Type> GetRoomTypesWithoutWarehouse()
        {
            List<Room.Type> roomTypes = new List<Room.Type>();
            roomTypes.Add(Room.Type.WaitingRoom);
            roomTypes.Add(Room.Type.OperatingRoom);
            roomTypes.Add(Room.Type.PatientCareRoom);
            roomTypes.Add(Room.Type.ConsultingRoom);
            return roomTypes;
        }

        public static List<Room> GetAllRooms(bool forCli)
        {
            string fileName = "../../../Data/Rooms/Rooms.json";
            if (forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/Rooms.json"; }
            List<Room> rooms = Deserialize(fileName);
            return rooms;
        }

        public static Room FindRoomById(int id)
        {
            string fileName = "../../../Data/Rooms/Rooms.json";
            List<Room> rooms = Deserialize(fileName);
            foreach (Room room in rooms)
            {
                if (room.Id == id)
                {
                    return room;
                }
            }
            return null;
        }

        public static int GetNewId(bool forCli)
        {
            List<Room> allRooms = GetAllRooms(forCli);
            List<Room> futureRooms = new List<Room>();
            List<RoomCombiningSchedule> comininngSchedules = RoomCombiningSchedule.GetCombiningRenovations(forCli);
            for (int i = 0; i < comininngSchedules.Count(); i++)
            {
                Room resulting = comininngSchedules.ElementAt(i).resultingRoom;
                futureRooms.Add(resulting);
            }
            int id = 1;
            bool taken = false;
            while (true)
            {
                for (int i = 0; i < allRooms.Count; i++)
                {
                    if (allRooms[i].Id == id) { taken = true; break; }
                }
                for (int i = 0; i < futureRooms.Count; i++)
                {
                    if (futureRooms[i].Id == id) { taken = true; break; }
                }
                if (!taken)
                {
                    return id;
                }
                taken = false;
                id++;
            }
        }
    }
}
