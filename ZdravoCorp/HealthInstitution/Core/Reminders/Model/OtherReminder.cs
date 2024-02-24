using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Reminders.Model
{
    public class OtherReminder
    {
        public int patientId;
        public int timer;
        public string name;
        public string description;
        public string startDate;
        public int days;
        public int times;

        public OtherReminder(string name, string description, string startDate, int days, int times, int patientId, int timer)
        {
            this.timer = timer;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
            this.days = days;
            this.times = times;
            this.patientId = patientId;
        }

        public int PatientId { get { return patientId; } set { patientId = value; } }
        public int Timer { get { return timer; } set { timer = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string StartDate { get { return startDate; } set { startDate = value; } }
        public int Days { get { return days; } set { days = value; } }
        public int Times { get { return times; } set { times = value; } }

        public static OtherReminder SetOtherReminder(OtherReminder reminder, string name,
            string description, string startDate, int days, int times, int patientId, int timer)
        {
            reminder.name = name;
            reminder.description = description;
            reminder.startDate = startDate;
            reminder.days = days;
            reminder.timer = timer;
            reminder.times = times;
            reminder.patientId = patientId;
            return reminder;
        }

        public static OtherReminder[] Load()
        {
            var jsontext = File.ReadAllText("../../../Data/Reminders/OtherReminder.json");
            OtherReminder[] allReminders = JsonConvert.DeserializeObject<OtherReminder[]>(jsontext)!;
            return allReminders;
        }

        public static void Save(OtherReminder[] reminders)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string remindersJson = System.Text.Json.JsonSerializer.Serialize(reminders, options);
            File.WriteAllText("../../../Data/Reminders/OtherReminder.json", remindersJson);
        }
    }
}
