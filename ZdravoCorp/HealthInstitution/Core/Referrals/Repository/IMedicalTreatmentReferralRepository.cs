using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Repository
{
    public interface IMedicalTreatmentReferralRepository
    {
        MedicalTreatmentReferral[] LoadFile();
        void WriteFile(MedicalTreatmentReferral[] referrals);
        void AddTreatmentReferral(MedicalTreatmentReferral treatmentReferral);
        List<MedicalTreatmentReferral> GetTreatments(int docId);
        void UpdateTreatment(MedicalTreatmentReferral treatment);

    }
}
