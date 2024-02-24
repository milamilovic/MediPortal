using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model
{
    public class DailyReport
    {
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int ReferralId { get; set; }
        public string Date { get; set; }
        public double HeartPressure { get; set; }
        public double Temperature { get; set; }
        public string Observations { get; set; }

        public DailyReport(int patientId, int referralId, double heartPressure, double temperature, string observations)
        {
            DateTime now = DateTime.Now;
            Random random = new Random();

            VisitId = random.Next(10000, 999999);
            PatientId = patientId;
            ReferralId = referralId;
            Date = now.ToString("dd.MM.yyyy."); ;
            HeartPressure = heartPressure;
            Temperature = temperature;
            Observations = observations;
        }
    }
}
