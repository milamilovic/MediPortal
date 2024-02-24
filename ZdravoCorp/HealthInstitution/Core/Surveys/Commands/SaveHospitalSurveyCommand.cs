using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;
using ZdravoCorp.HealthInstitution.Core.Surveys.Services;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Commands
{
    public class SaveHospitalSurveyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HospitalSurveyViewModel _viewModel;

        public SaveHospitalSurveyCommand(HospitalSurveyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            SurveyRepository hospitalSurveyRepository = new SurveyRepository();
            HospitalSurveyService hospService = new HospitalSurveyService(_viewModel, hospitalSurveyRepository);
            SurveyService service = new SurveyService();
            // if patient did not answer all required questions
            if (hospService.GetAnswers().Count == 0)
            {
                MessageBox.Show("You must answer all required questions!\nOnly comment is not required.", "Warning");
            }
            else
            {
                hospService.SaveHospitalSurvey(_viewModel.Patient.Id, hospService.GetAnswers(),
                    service.GetQuestions(_viewModel.Question1, _viewModel.Question2, _viewModel.Question3, 
                    _viewModel.Question4), _viewModel.Comment);
                MessageBox.Show("Saved survey.\nThank You very much for Your time!", "Confirmation");
                _viewModel.InitializeEmptyHospitalSurvey();
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
