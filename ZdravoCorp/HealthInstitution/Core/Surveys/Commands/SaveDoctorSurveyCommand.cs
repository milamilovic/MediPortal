using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;
using ZdravoCorp.HealthInstitution.Core.Surveys.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Commands
{
    public class SaveDoctorSurveyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private DoctorSurveyViewModel _viewModel;

        public SaveDoctorSurveyCommand(DoctorSurveyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyRepository();
            DoctorSurveyService docService = new DoctorSurveyService(_viewModel, doctorSurveyRepository);
            SurveyService service = new SurveyService();
            // if patient did not answer all required questions
            if (docService.GetAnswers().Count == 0)
            {
                MessageBox.Show("You must answer all required questions!\nOnly comment is not required.", "Warning");
            }
            else
            {
                docService.SaveDoctorSurvey(_viewModel.Patient.Id, _viewModel.Doctor.Id,
                    docService.GetAnswers(), service.GetQuestions(_viewModel.Question1, _viewModel.Question2), _viewModel.Comment);
                _viewModel.Doctor.Rates.AddRange(docService.GetAnswers());
                Doctor.SetAverageRate(_viewModel.Doctor);
                MessageBox.Show("Saved survey.\nThank You very much for Your time!", "Confirmation");
                _viewModel.InitializeEmptyDoctorSurvey();
            }
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
