using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Services
{
    public class DoctorVisitsService
    {
        private readonly IMedicalTreatmentReferralRepository _treatmentRepository;
        public DoctorVisitsService(IMedicalTreatmentReferralRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }
        internal void UpdateTreatment(MedicalTreatmentReferral treatment)
        {
            _treatmentRepository.UpdateTreatment(treatment);
        }

        public List<MedicalTreatmentReferral> LoadCurrentTreatments(int docId)
        {
            return _treatmentRepository.GetTreatments(docId);
        }
    }
}
