using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Repository
{
    public class PatientOnTreatmentRepository
    {
        public static List<PatientOnTreatmentItem> LoadPatientsCurrentlyOnTreatment()
        {
            List<PatientOnTreatmentItem> patientsOnTreatment = new List<PatientOnTreatmentItem>();
            MedicalTreatmentReferralRepository medicalTreatmentReferralRepository = new MedicalTreatmentReferralRepository();
            MedicalTreatmentReferral[] referrals = medicalTreatmentReferralRepository.LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                if (referral.TreatmentStarted && !referral.TreatmentEnded)
                {
                    Patient patient = Patient.Find(referral.PatientId);
                    PatientOnTreatmentItem patientOnTreatment = new PatientOnTreatmentItem(patient.MedicalRecord.Name + " " + patient.MedicalRecord.Surname, referral.RoomId, referral.Therapy, referral.TreatmentId);
                    patientsOnTreatment.Add(patientOnTreatment);
                }
            }

            return patientsOnTreatment;
        }
    }
}
