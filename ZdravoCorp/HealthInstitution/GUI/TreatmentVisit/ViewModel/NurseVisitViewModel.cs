using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;

namespace ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel
{
    public class NurseVisitViewModel
    {
        public PatientOnTreatmentItem SelectedPatientOnTreatment;
        public double HeartPressure { get; set; }
        public double Temperature { get; set; }
        public String Observations { get; set; }
        public DateTime NewDate { get; set; }

        public SubmitDailyReportCommand SubmitDailyReportCommand { get; }
        
        public NurseVisitViewModel(PatientOnTreatmentItem patient) { 
            SelectedPatientOnTreatment = patient;
            SubmitDailyReportCommand = new SubmitDailyReportCommand(this);
        }
    }
}
