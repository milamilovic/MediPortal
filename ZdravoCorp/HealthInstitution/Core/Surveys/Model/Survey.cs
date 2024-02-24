using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public class Survey : MainSurvey
    {
        public Survey(int surveyId, int patientId, string date, List<string> questions, List<int> answers, string comment)
            : base(surveyId, patientId, date, questions, answers, comment)
        {
        }
    }
}
