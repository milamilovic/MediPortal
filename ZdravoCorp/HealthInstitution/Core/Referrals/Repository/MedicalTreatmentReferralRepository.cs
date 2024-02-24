using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Repository
{
    public class MedicalTreatmentReferralRepository : IMedicalTreatmentReferralRepository
    {
        public MedicalTreatmentReferral[] LoadFile()
        {
            var jsontext = File.ReadAllText("../../../Data/Referrals/MedicalTreatmentReferral.json");
            MedicalTreatmentReferral[] referrals = JsonConvert.DeserializeObject<MedicalTreatmentReferral[]>(jsontext)!;
            return referrals;
        }

        public void WriteFile(MedicalTreatmentReferral[] referrals)
        {
            string fileName = "../../../Data/Referrals/MedicalTreatmentReferral.json";
            dynamic text = JsonConvert.SerializeObject(referrals, Formatting.Indented);
            File.WriteAllText(fileName, text);

        }
        public void AddTreatmentReferral(MedicalTreatmentReferral treatmentReferral)
        {
            MedicalTreatmentReferral[] referrals = LoadFile();
            referrals = referrals.Concat(new MedicalTreatmentReferral[] { treatmentReferral }).ToArray();
            WriteFile(referrals);

        }

        public void UpdateTreatment(MedicalTreatmentReferral treatment)
        {
            MedicalTreatmentReferral[] referrals = LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                if (referral.Equals(treatment))
                {
                    referral.Therapy = treatment.Therapy;   //if therapy is being updated
                    referral.EndDate = treatment.EndDate;     //if date is being updated
                    referral.Days = treatment.Days;         //update number of days
                    referral.TreatmentEnded = treatment.TreatmentEnded;
                }
            }
            WriteFile(referrals);
        }

        public void UpdateTreatmentStarted(MedicalTreatmentReferral treatment)
        {
            MedicalTreatmentReferral[] referrals = LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                if (referral.TreatmentId == treatment.TreatmentId)
                {
                    referral.TreatmentEnded = treatment.TreatmentEnded;
                    referral.TreatmentStarted = treatment.TreatmentStarted;
                    referral.RoomId = treatment.RoomId;
                }
            }
            File.WriteAllText(@"../../../Data/Referrals/MedicalTreatmentReferral.json", JsonConvert.SerializeObject(referrals, Formatting.Indented));
        }

        public MedicalTreatmentReferral GetTreatmentById(int id)
        {
            MedicalTreatmentReferral[] referrals = LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                if (referral.TreatmentId == id)
                    return referral;
            }
            return null;
        }

        public List<MedicalTreatmentReferral> GetTreatments(int docId)
        {
            List<MedicalTreatmentReferral> currentRefferals = new List<MedicalTreatmentReferral>();
            MedicalTreatmentReferral[] referrals = LoadFile();
            foreach (MedicalTreatmentReferral referral in referrals)
            {
                if (referral.TreatmentEnded == false && referral.DoctorId == docId && referral.TreatmentStarted==true)
                {
                    currentRefferals.Add(referral);
                }
            }

            return currentRefferals;
        }
    }
}
