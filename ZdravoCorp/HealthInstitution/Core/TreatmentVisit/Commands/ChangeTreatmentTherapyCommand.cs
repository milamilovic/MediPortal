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
    public class ChangeTreatmentTherapyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DoctorVisitsViewModel viewModel;
        public ChangeTreatmentTherapyCommand(DoctorVisitsViewModel viewModel)
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
                if (viewModel.NewTherapy != "")
                {
                    treatment.Therapy = viewModel.NewTherapy;
                    viewModel.VisitsService.UpdateTreatment(treatment);
                    MessageBox.Show("Successfully updated patients therapy");
                    viewModel.InitializeTreatments();
                }
                else
                {
                    MessageBox.Show("Cant update therapy");
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
