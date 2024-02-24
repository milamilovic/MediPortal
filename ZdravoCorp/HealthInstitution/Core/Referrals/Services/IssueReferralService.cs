using Newtonsoft.Json;
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
    public class IssueReferralService
    {
        private readonly IMedicalTreatmentReferralRepository _treatmentRepository;
        private readonly IExaminationReferralRepository _examinationRepository;
        public IssueReferralService(IMedicalTreatmentReferralRepository treatmentRepository,
           IExaminationReferralRepository examinationRepository)
        {
            _treatmentRepository = treatmentRepository;
            _examinationRepository =examinationRepository;
        }
        public static bool CheckDates(DateTime selectedDateFrom, DateTime selectedDateTo, int days)
        {
            if (selectedDateTo > DateTime.Now)
            {
                if (selectedDateTo > selectedDateFrom)
                {
                    if ((selectedDateTo - selectedDateFrom).Days == days)
                        return true;
                }
            }
            return false;
        }
        public void IssueExaminationReferral(Doctor doctor, int patientId, string date, string description)
        {
            ExaminationReferral referral = new ExaminationReferral(patientId, doctor.Id, date, description, false);
            _examinationRepository.AddExaminationReferral(referral);
        }

        public void IssueMedicalTreatment(int doctorId, int patientId, int days,
            string therapy, string additionalExams, string startDate, string endDate)
        {
            MedicalTreatmentReferral referral = new MedicalTreatmentReferral(patientId, doctorId, days, therapy,
                additionalExams, startDate, endDate);
            _treatmentRepository.AddTreatmentReferral(referral);
        }
    }
}
