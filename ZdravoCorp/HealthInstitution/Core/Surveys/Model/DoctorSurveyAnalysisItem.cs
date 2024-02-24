using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public class DoctorSurveyAnalysisItem
    {
        public double average;
        public int fives;
        public int fours;
        public int threes;
        public int twos;
        public int ones;
        public string question;
        public int doctorId;

        public DoctorSurveyAnalysisItem(double average, int fives, int fours, int threes, int twos, int ones, string question, int doctorId)
        {
            this.average = average;
            this.fives = fives;
            this.fours = fours;
            this.threes = threes;
            this.twos = twos;
            this.ones = ones;
            this.question = question;
            this.doctorId = doctorId;
        }
    }
}
