using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public abstract class MainSurvey
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Date { get; set; }
        public List<string> Questions { get; set; }
        public List<int> Answers { get; set; }
        public string Comment { get; set; }

        public MainSurvey(int surveyId, int patientId, string date, List<string> questions, List<int> answers, string comment)
        {
            Id = surveyId;
            PatientId = patientId;
            Date = date;
            Questions = questions;
            Answers = answers;
            Comment = comment;
        }
    }
}
