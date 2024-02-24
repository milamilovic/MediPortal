using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Services
{
    public class HospitalSurveyService
    {
        private readonly ISurveyRepository _hospitalRepository;
        private HospitalSurveyViewModel _viewModel;
        private SurveyService _surveyService = new SurveyService();

        public HospitalSurveyService(HospitalSurveyViewModel viewModel, ISurveyRepository hospitalRepository)
        {
            _viewModel = viewModel;
            _hospitalRepository = hospitalRepository;
        }

        public List<int> GetAnswers()
        {
            List<int> answers = new List<int>();
            int answer1 = _surveyService.GetRating(_viewModel.IsCheckedQ1R1, _viewModel.IsCheckedQ1R2,
                _viewModel.IsCheckedQ1R3, _viewModel.IsCheckedQ1R4, _viewModel.IsCheckedQ1R5);
            int answer2 = _surveyService.GetRating(_viewModel.IsCheckedQ2R1, _viewModel.IsCheckedQ2R2,
                _viewModel.IsCheckedQ2R3, _viewModel.IsCheckedQ2R4, _viewModel.IsCheckedQ2R5);
            int answer3 = _surveyService.GetRating(_viewModel.IsCheckedQ3R1, _viewModel.IsCheckedQ3R2,
                _viewModel.IsCheckedQ3R3, _viewModel.IsCheckedQ3R4, _viewModel.IsCheckedQ3R5);
            int answer4 = _surveyService.GetRating(_viewModel.IsCheckedQ4R1, _viewModel.IsCheckedQ4R2,
                _viewModel.IsCheckedQ4R3, _viewModel.IsCheckedQ4R4, _viewModel.IsCheckedQ4R5);
            if (answer1 != 0 && answer2 != 0 && answer3 != 0 && answer4 != 0)
            {
                answers.Add(answer1);
                answers.Add(answer2);
                answers.Add(answer3);
                answers.Add(answer4);
            }
            return answers;
        }

        public void SaveHospitalSurvey(int patientId, List<int> answers, List<string> questions, string comment)
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy.");
            Survey newSurvey = new Survey(_hospitalRepository.GetNextSurveyId(), patientId, date, questions, answers, comment);
            _hospitalRepository.Add(newSurvey);
        }
    }
}
