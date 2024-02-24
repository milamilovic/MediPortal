using System.Data;
using System.Windows.Documents;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public class HospitalSurveyAnalysisItem
    {
        public double average;
        public int fives;
        public int fours;
        public int threes;
        public int twos;
        public int ones;
        public string question;

        public HospitalSurveyAnalysisItem(double average, int fives, int fours, int threes, int twos, int ones, string question)
        {
            this.average = average;
            this.fives = fives;
            this.fours = fours;
            this.threes = threes;
            this.twos = twos;
            this.ones = ones;
            this.question = question;
        }
    }
}