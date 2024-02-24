using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Model
{
    public class ExaminationReferral
    {
        [JsonProperty]
        public int PatientId { get; set; }
        [JsonProperty]
        public int DoctorId { get; set; }
        [JsonProperty]
        public string Date { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public bool WasUsed { get; set; }

        public ExaminationReferral(int patientId, int doctorId, string date, string description, bool wasUsed)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Description = description;
            WasUsed = wasUsed;
        }
    }
}
