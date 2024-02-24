using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;
using ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.DaysOff.Commands
{
    public class RejectDaysOffCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DaysOfftReviewViewModel viewModel;

        public RejectDaysOffCommand(DaysOfftReviewViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int requestId = viewModel.SelectedRequest.RequestId;
            var allDaysOff = DaysOffRepository.Deserialize();
            foreach (DaysOffRequest dayOff in allDaysOff)
            {
                if (dayOff.RequestId == requestId)
                {
                    allDaysOff.Remove(dayOff);
                    DaysOffRepository.Serialize(allDaysOff);
                    break;
                }
            }
            MessageBox.Show("Days off request with id " + requestId + " is rejected!");
            viewModel.InitializeRequests();
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
