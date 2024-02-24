using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel
{
    public class DoctorVisitsGridViewModel : INotifyPropertyChanged
    {
        internal DoctorVisitsGridViewModel(MedicalTreatmentReferral medicalTreatmentReferral)
        {
            DoctorId = medicalTreatmentReferral.DoctorId;
            PatientId = medicalTreatmentReferral.PatientId;
            Days = medicalTreatmentReferral.Days;
            Therapy = medicalTreatmentReferral.Therapy;
            AdditionalExams = medicalTreatmentReferral.AdditionalExams;
            StartDate = medicalTreatmentReferral.StartDate;
            EndDate = medicalTreatmentReferral.EndDate;
        }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int Days { get; set; }
        public string Therapy { get; set; }
        public string AdditionalExams { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
