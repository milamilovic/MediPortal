using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Services
{
    internal class MedicalTreatmentReferralService
    {
        public static void UpdateTreatments()
        {
            DateTime now = DateTime.Now;
            MedicalTreatmentReferralRepository repository = new MedicalTreatmentReferralRepository();
            MedicalTreatmentReferral[] referrals = repository.LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                DateTime TreatmentEndDate = DateTime.ParseExact(referral.EndDate, "dd.MM.yyyy.", null);
                if (TreatmentEndDate.Date<now.Date && referral.TreatmentEnded==false)
                {
                    referral.TreatmentEnded = true;
                    repository.UpdateTreatmentStarted(referral);
                }

            }
        }

        public static MedicalTreatmentReferral FindPatientsReferral(Patient patient)
        {
            DateTime today = DateTime.Today;
            MedicalTreatmentReferralRepository repository = new MedicalTreatmentReferralRepository();
            MedicalTreatmentReferral[] referrals = repository.LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                DateTime TreatmentStartDate = DateTime.ParseExact(referral.StartDate, "dd.MM.yyyy.", null);
                if (referral.PatientId == patient.Id && !referral.TreatmentStarted && today.Equals(TreatmentStartDate))
                    return referral;
            }

            return null;
        }
    }
}
