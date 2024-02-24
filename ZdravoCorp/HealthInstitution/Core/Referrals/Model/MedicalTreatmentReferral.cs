using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Model
{
    public class MedicalTreatmentReferral
    {
        [JsonProperty]
        public int TreatmentId { get; set; }
        [JsonProperty]
        public int PatientId { get; set; }
        [JsonProperty]
        public int DoctorId { get; set; }
        [JsonProperty]
        public int Days { get; set; }
        [JsonProperty]
        public string Therapy { get; set; }
        [JsonProperty]
        public string AdditionalExams { get; set; }
        [JsonProperty]
        public string StartDate { get; set; }
        [JsonProperty]
        public string EndDate { get; set; }
        [JsonProperty]
        public bool TreatmentEnded { get; set; }
        [JsonProperty]
        public bool TreatmentStarted { get; set; }
        [JsonProperty]
        public int RoomId { get; set; }

        public MedicalTreatmentReferral(int patientId, int doctorId, int days, string therapy,
            string additionalExams, string startDate, string endDate)
        {
            Random random = new Random();
            TreatmentId = random.Next(10000, 999999);
            PatientId = patientId;
            DoctorId = doctorId;
            Days = days;
            Therapy = therapy;
            AdditionalExams = additionalExams;
            StartDate = startDate;
            EndDate = endDate;
            TreatmentEnded = false;
            TreatmentStarted = false;
            RoomId = -1;
        }

        public override bool Equals(object? obj)
        {
            return obj is MedicalTreatmentReferral referral &&
                   PatientId == referral.PatientId &&
                   DoctorId == referral.DoctorId &&
                   AdditionalExams == referral.AdditionalExams &&
                   StartDate == referral.StartDate;
        }
    }
}
