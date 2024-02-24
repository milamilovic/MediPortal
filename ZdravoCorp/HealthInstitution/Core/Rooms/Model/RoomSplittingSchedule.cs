using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{
    public class RoomSplittingSchedule
    {
        public int roomId { get; set; }
        public Room resultingRoom { get; set; }

        public TimeSlot Time { get; set; }

        public RoomSplittingSchedule(int firstRoom, Room resultingRoom, TimeSlot time)
        {
            roomId = firstRoom;
            this.resultingRoom = resultingRoom;
            Time = time;
        }

        private static void Serialize(List<RoomSplittingSchedule> appointments, bool forCli)
        {
            string fileName = "../../../Data/Rooms/SplittingRoomRenovations.json";
            if(forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/SplittingRoomRenovations.json"; }
            dynamic text = JsonConvert.SerializeObject(appointments, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        private static List<RoomSplittingSchedule> Deserialize(bool forCli)
        {
            string fileName = "../../../Data/Rooms/SplittingRoomRenovations.json";
            if (forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/SplittingRoomRenovations.json"; }
            var jsontext = File.ReadAllText(fileName);
            List<RoomSplittingSchedule> appointments = JsonConvert.DeserializeObject<List<RoomSplittingSchedule>>(jsontext)!;
            return appointments;
        }

        public static List<RoomSplittingSchedule> GetSplittingRenovations(bool forCli)
        {
            return Deserialize(forCli);
        }

        public static void Save(List<RoomSplittingSchedule> appointments, bool forCli)
        {
            Serialize(appointments, forCli);
            return;
        }

        public static void AddRenovation(RoomSplittingSchedule appointment, bool forCli)
        {
            List<RoomSplittingSchedule> appointments = GetSplittingRenovations(forCli);
            appointments.Add(appointment);
            Save(appointments, forCli);
            return;
        }
    }
}
