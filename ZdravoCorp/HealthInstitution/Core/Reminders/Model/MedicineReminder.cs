using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;

namespace ZdravoCorp.HealthInstitution.Core.Reminders.Model
{
    public class MedicineReminder : INotifyPropertyChanged
    {
        public Prescription prescription;
        public int timer;

        public MedicineReminder(Prescription prescription)
        {
            this.prescription = prescription;
            timer = 0;
        }

        public int Timer
        {
            get => timer;
            set
            {
                if (value != timer)
                {
                    timer = value;
                    OnPropertyChanged();
                }

            }
        }

        public Prescription Prescription { get => prescription; set => prescription = value; }

        public static MedicineReminder[] Load()
        {
            var jsontext = File.ReadAllText("../../../Data/Reminders/MedicineReminder.json");
            MedicineReminder[] allReminders = JsonConvert.DeserializeObject<MedicineReminder[]>(jsontext)!;
            return allReminders;
        }

        public static void Save(MedicineReminder[] reminders)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string remindersJson = System.Text.Json.JsonSerializer.Serialize(reminders, options);
            File.WriteAllText("../../../Data/Reminders/MedicineReminder.json", remindersJson);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
