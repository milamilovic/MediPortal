using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Services
{
    public class DoctorSurveyService
    {
        private readonly IDoctorSurveyRepository _surveyRepository;
        private DoctorSurveyViewModel _viewModel;
        private SurveyService _surveyService = new SurveyService();
        public DoctorSurveyService(DoctorSurveyViewModel viewModel, IDoctorSurveyRepository doctorSurveyRepository)
        {
            _viewModel = viewModel;
            _surveyRepository = doctorSurveyRepository;
        }
        

        public List<int> GetAnswers()
        {
            List<int> answers = new List<int>();
            int answer1 = _surveyService.GetRating(_viewModel.IsCheckedQ1R1, _viewModel.IsCheckedQ1R2, 
                _viewModel.IsCheckedQ1R3, _viewModel.IsCheckedQ1R4, _viewModel.IsCheckedQ1R5);
            int answer2 = _surveyService.GetRating(_viewModel.IsCheckedQ2R1, _viewModel.IsCheckedQ2R2, 
                _viewModel.IsCheckedQ2R3, _viewModel.IsCheckedQ2R4, _viewModel.IsCheckedQ2R5);
            if (answer1 != 0 && answer2 != 0)
            {
                answers.Add(answer1);
                answers.Add(answer2);
            }
            return answers;
        }

        public void SaveDoctorSurvey(int patientId, int doctorId, List<int> answers, List<string> questions, string comment)
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy.");
            DoctorSurvey newSurvey = new DoctorSurvey(_surveyRepository.GetNextDoctorSurveyId(), patientId, date, questions, answers, comment, doctorId);
            _surveyRepository.Add(newSurvey);
        }

        
    }
}
