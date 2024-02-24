using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;

namespace ZdravoCorp.HealthInstitution.Core.Prescriptions.Model
{
    public class Prescription
    {
        public int PatientId { get; set; }

        public Medicine Medicine { get; set; }
        public int Days { get; set; }
        public string Schedule { get; set; }
        public string Meals { get; set; }
        public string Date { get; set; }
        public bool WasUsed { get; set; }

        public Prescription(int patientId, Medicine medicine, int days, string schedule, string meals, string date, bool wasUsed)
        {
            PatientId = patientId;
            Medicine = medicine;
            Days = days;
            Schedule = schedule;
            Meals = meals;
            Date = date;
            WasUsed = wasUsed;
        }
        public static Prescription[] LoadFile()
        {
            var jsontext = File.ReadAllText("../../../Data/Prescriptions/Prescription.json");
            Prescription[] prescriptions = JsonConvert.DeserializeObject<Prescription[]>(jsontext)!;
            return prescriptions;
        }
        public static void WriteFile(Prescription[] prescriptions)
        {
            File.WriteAllText(@"../../../Data/Prescriptions/Prescription.json", JsonConvert.SerializeObject(prescriptions, Formatting.Indented));

        }
    }
}
