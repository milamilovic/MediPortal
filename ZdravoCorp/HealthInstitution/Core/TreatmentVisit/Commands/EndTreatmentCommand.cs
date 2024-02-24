using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Services;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands
{
    public class EndTreatmentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DoctorVisitsViewModel viewModel;

        public EndTreatmentCommand(DoctorVisitsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (viewModel.SelectedTreatment != null)
            {
                MedicalTreatmentReferral treatment = viewModel.ConvertToTreatment();
                string date = DateTime.Now.ToString("dd.MM.yyyy.");
                treatment.EndDate = date;
                int days = (DateTime.Parse(treatment.EndDate) - DateTime.Parse(treatment.StartDate)).Days;
                treatment.Days = days;
                treatment.TreatmentEnded = true;

                viewModel.VisitsService.UpdateTreatment(treatment);
                MessageBox.Show("Successfully ended treatment");
                viewModel.InitializeTreatments();

                MessageBoxResult result = MessageBox.Show("Schedule a check-up", "Check-up", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string checkUpDate = DateTime.Now.AddDays(10).ToString("dd.MM.yyyy.");
                    TimeSlot availableTimeSlot = TimeSlot.FindFirstAvailable(viewModel.DocId, checkUpDate);

                    Examination.Create(Examination.GetNextExaminationId(), false, viewModel.DocId, treatment.PatientId, availableTimeSlot);
                }
            }
            else { MessageBox.Show("Select a row in the data grid"); }

        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
