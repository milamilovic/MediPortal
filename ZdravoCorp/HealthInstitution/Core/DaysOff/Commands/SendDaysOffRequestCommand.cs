using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Services;

namespace ZdravoCorp.HealthInstitution.Core.DaysOff.Commands
{
    internal class SendDaysOffRequestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DoctorDaysOffViewModel viewModel;

        public SendDaysOffRequestCommand(DoctorDaysOffViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (viewModel.Reason != "")
            {
                if (RequestDaysOffService.CheckDates(viewModel.StartDate, viewModel.EndDate))
                {
                    RequestDaysOffService.SendRequest(viewModel.StartDate, viewModel.EndDate,
                        viewModel.Reason, viewModel.DocId);
                    MessageBox.Show("Request sent!");
                }
                else
                {
                    MessageBox.Show("Invalid dates");
                }
            }
            else
            {
                MessageBox.Show("Input required data");
            }

        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
