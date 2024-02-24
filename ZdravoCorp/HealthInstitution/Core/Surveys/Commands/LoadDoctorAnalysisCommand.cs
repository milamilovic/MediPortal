using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;
using ZdravoCorp.HealthInstitution.Surveys;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Commands
{
    public class LoadDoctorAnalysisCommand : ICommand
    {
        private DoctorSurveyAnalysesViewModel doctorSurveyAnalysesViewModel;

        public LoadDoctorAnalysisCommand(DoctorSurveyAnalysesViewModel hospitalSurveyAnalysesViewModel)
        {
            doctorSurveyAnalysesViewModel = hospitalSurveyAnalysesViewModel;
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

        public double GetAverage(ref List<DoctorSurvey> surveys, int docId)
        {
            int numberOfQuestions = surveys[0].Answers.Count;
            double average = 0;
            int count = 0;
            foreach (DoctorSurvey survey in surveys)
            {
                if (survey.DoctorId == docId)
                {
                    count++;
                    for (int i = 0; i < numberOfQuestions; i++)
                    {
                        average += survey.Answers[i];
                    }
                }
            }
            average = average / count;
            return average;
        }

        public List<int> RatesForQuestionDoctor(ref List<DoctorSurvey> allSurveys, int docId, int index)
        {
            List<int> rates = new List<int> { 0, 0, 0, 0, 0 };
            List<DoctorSurvey> surveys = new List<DoctorSurvey>();
            foreach (DoctorSurvey doctorSurvey in allSurveys)
            {
                if (doctorSurvey.DoctorId == docId) { surveys.Add(doctorSurvey); }
            }
            foreach (DoctorSurvey survey in surveys)
            {
                rates[survey.Answers[index] - 1]++;
            }

            return rates;
        }

        internal List<int> GetDoctors(ref List<DoctorSurvey> surveys)
        {
            List<int> ids = new List<int>();
            foreach (DoctorSurvey survey in surveys)
            {
                if (!ids.Contains(survey.DoctorId))
                {
                    ids.Add(survey.DoctorId);
                }
            }
            return ids;
        }
    }
}
