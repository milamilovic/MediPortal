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
using System.Windows.Markup;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{
    public class RoomSchedule
    {
        public int roomId { get; set; }
        public int appointmentId { get; set; }

        public TimeSlot Time { get; set; }

        public RoomSchedule(int roomId, int appointmentId, TimeSlot time)
        {
            this.roomId = roomId;
            this.appointmentId = appointmentId;
            Time = time;
        }

        private static void SerializeSimpleRenovation(List<RoomSchedule> appointments, bool forCli)
        {
            string fileName = "../../../Data/Rooms/RoomAppointments.json";
            if(forCli) fileName = "../../../../ZdravoCorp/Data/Rooms/RoomAppointments.json";
            dynamic text = JsonConvert.SerializeObject(appointments, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        private static List<RoomSchedule> DeserializeSimpleRenovation(bool forCli)
        {
            string fileName = "../../../Data/Rooms/RoomAppointments.json";
            if(forCli) { fileName = "../../../../ZdravoCorp/Data/Rooms/RoomAppointments.json"; }
            var jsontext = File.ReadAllText(fileName);
            List<RoomSchedule> appointments = JsonConvert.DeserializeObject<List<RoomSchedule>>(jsontext)!;
            return appointments;
        }

        public static List<RoomSchedule> GetAppointments(bool forCli)
        {
            return DeserializeSimpleRenovation(forCli);
        }

        public static void SaveSimpleAppointments(List<RoomSchedule> appointments, bool forCli)
        {
            SerializeSimpleRenovation(appointments, forCli);
            return;
        }
        public static int FindRoom(Examination exam)
        {
            List<RoomSchedule> examRooms = GetAppointments(false);
            foreach (RoomSchedule examRoom in examRooms)
            {
                if (examRoom.appointmentId == exam.Id)
                {
                    return examRoom.roomId;
                }
            }
            return 0;
        }

        public static void AddSimpleAppointment(RoomSchedule appointment, bool forCli)
        {
            List<RoomSchedule> appointments = GetAppointments(forCli);
            appointments.Add(appointment);
            SaveSimpleAppointments(appointments, forCli);
            return;
        }

        public static bool CheckOverlap(RoomSchedule exitstingAppointment, RoomSchedule newAppointment)
        {
            DateTime startTimeExisting = TimeSlot.GetStartTime(exitstingAppointment.Time);
            DateTime endTimeExisting = TimeSlot.GetEndTime(exitstingAppointment.Time);
            DateTime startTimeNew = TimeSlot.GetStartTime(newAppointment.Time);
            DateTime endTimeNew = TimeSlot.GetEndTime(newAppointment.Time);

            bool overlap = startTimeExisting < endTimeNew && startTimeNew < endTimeExisting;

            return overlap;
        }

        public static bool CheckOverlap(RoomSchedule exitstingAppointment, TimeSlot newAppointment)
        {
            DateTime startTimeExisting = TimeSlot.GetStartTime(exitstingAppointment.Time);
            DateTime endTimeExisting = TimeSlot.GetEndTime(exitstingAppointment.Time);
            DateTime startTimeNew = TimeSlot.GetStartTime(newAppointment);
            DateTime endTimeNew = TimeSlot.GetEndTime(newAppointment);

            bool overlap = startTimeExisting < endTimeNew && startTimeNew < endTimeExisting;

            return overlap;
        }

        public static bool CheckOverlap(TimeSlot exitstingAppointment, TimeSlot newAppointment)
        {
            DateTime startTimeExisting = TimeSlot.GetStartTime(exitstingAppointment);
            DateTime endTimeExisting = TimeSlot.GetEndTime(exitstingAppointment);
            DateTime startTimeNew = TimeSlot.GetStartTime(newAppointment);
            DateTime endTimeNew = TimeSlot.GetEndTime(newAppointment);

            bool overlap = startTimeExisting < endTimeNew && startTimeNew < endTimeExisting;

            return overlap;
        }
    }
}
