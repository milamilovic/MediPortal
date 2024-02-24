using System;
using System.Collections.Generic;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Commands
{
    public class LoadHospitalAnalysisCommand : ICommand
    {
        private HospitalSurveyAnalysesViewModel hospitalSurveyAnalysesViewModel;

        public LoadHospitalAnalysisCommand(HospitalSurveyAnalysesViewModel hospitalSurveyAnalysesViewModel)
        {
            this.hospitalSurveyAnalysesViewModel = hospitalSurveyAnalysesViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            return;
        }

        public List<double> GetAverages(ref List<Survey> surveys)
        {
            int numberOfQuestions = surveys[0].Answers.Count;
            List<double> averages = new List<double>();
            foreach (int ans in surveys[0].Answers) { averages.Add(0); }
            foreach (Survey survey in surveys)
            {
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    averages[i] += survey.Answers[i];
                }
            }
            for (int i = 0; i < averages.Count; i++)
            {
                averages[i] = averages[i] / surveys.Count;
            }
            return averages;
        }

        public List<int> RatesForQuestionHospital(int index, ref List<Survey> allSurveys)
        {
            List<int> rates = new List<int> { 0, 0, 0, 0, 0 };
            Survey survey = allSurveys[index];

            foreach (int i in survey.Answers)
            {
                rates[i - 1]++;
            }

            return rates;
        }
    }
}