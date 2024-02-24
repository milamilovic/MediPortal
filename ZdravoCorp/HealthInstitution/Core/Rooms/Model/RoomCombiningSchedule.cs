using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{
    public class RoomCombiningSchedule
    {
        public int firstRoomId { get; set; }
        public int secondRoomId { get; set; }
        public Room resultingRoom { get; set; }

        public TimeSlot Time { get; set; }

        public RoomCombiningSchedule(int firstRoom, int secondRoom, Room resultingRoom, TimeSlot time)
        {
            firstRoomId = firstRoom;
            secondRoomId = secondRoom;
            this.resultingRoom = resultingRoom;
            Time = time;
        }

        private static void Serialize(List<RoomCombiningSchedule> appointments, bool forCli)
        {
            string fileName = "../../../Data/Rooms/CombiningRoomRenovations.json";
            if(forCli) fileName = "../../../../ZdravoCorp/Data/Rooms/CombiningRoomRenovations.json";
            dynamic text = JsonConvert.SerializeObject(appointments, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        private static List<RoomCombiningSchedule> Deserialize(bool forCli)
        {
            string fileName = "../../../Data/Rooms/CombiningRoomRenovations.json";
            if(forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/CombiningRoomRenovations.json"; }
            var jsontext = File.ReadAllText(fileName);
            List<RoomCombiningSchedule> appointments = JsonConvert.DeserializeObject<List<RoomCombiningSchedule>>(jsontext)!;
            return appointments;
        }

        public static List<RoomCombiningSchedule> GetCombiningRenovations(bool forCli)
        {
            return Deserialize(forCli);
        }

        public static void Save(List<RoomCombiningSchedule> appointments, bool forCli)
        {
            Serialize(appointments, forCli);
            return;
        }

        public static void AddRenovation(RoomCombiningSchedule appointment, bool forCli)
        {
            List<RoomCombiningSchedule> appointments = GetCombiningRenovations(forCli);
            appointments.Add(appointment);
            Save(appointments, forCli);
            return;
        }

    }
}
