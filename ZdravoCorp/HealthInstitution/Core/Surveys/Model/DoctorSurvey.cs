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
    public class DoctorSurvey : MainSurvey
    {
        public int DoctorId { get; set; }
        public DoctorSurvey(int surveyId, int patientId, string date, List<string> questions, List<int> answers, string comment, int id) : base(surveyId, patientId, date, questions, answers, comment)
        {
            DoctorId = id;
        }
    }

}
