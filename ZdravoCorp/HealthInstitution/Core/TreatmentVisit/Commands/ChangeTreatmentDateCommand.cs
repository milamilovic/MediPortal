using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Services;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands
{
    public class ChangeTreatmentDateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DoctorVisitsViewModel viewModel;
        public ChangeTreatmentDateCommand(DoctorVisitsViewModel viewModel)
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
                if (viewModel.NewDate.Date > DateTime.Parse(treatment.EndDate).Date)
                {
                    string date = viewModel.NewDate.ToString("dd.MM.yyyy.");
                    treatment.EndDate = date;
                    int days = (DateTime.Parse(treatment.EndDate) - DateTime.Parse(treatment.StartDate)).Days;
                    treatment.Days = days;

                    viewModel.VisitsService.UpdateTreatment(treatment);
                    MessageBox.Show("Successfully extended treatment");
                    viewModel.InitializeTreatments();
                }
                else
                {
                    MessageBox.Show("Wrong date selected");
                }
            }
            else { MessageBox.Show("Select a row in data grid"); }

        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
