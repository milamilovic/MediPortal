using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model
{
    public class PatientOnTreatmentItem
    {
        public String PatientFullName { get; set; }
        public int RoomId { get; set; }
        public String Therapy { get; set; }
        public int ReferralId { get; set; }

        public PatientOnTreatmentItem(string patientFullName, int roomId, string therapy, int referralId)
        {
            PatientFullName = patientFullName;
            RoomId = roomId;
            Therapy = therapy;
            ReferralId = referralId;
        }
    }
}
