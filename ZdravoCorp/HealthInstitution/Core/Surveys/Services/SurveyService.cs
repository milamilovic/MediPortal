using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Services
{
    public class SurveyService
    {
        public SurveyService() { }

        // important order of params
        // can not be params RadioButton[]
        public int GetRating(bool isChecked1, bool isChecked2, bool isChecked3, bool isChecked4, bool isChecked5)
        {
            int rating = 0;
            if (isChecked1)
            {
                rating = 1;
            }
            else if (isChecked2)
            {
                rating = 2;
            }
            else if (isChecked3)
            {
                rating = 3;
            }
            else if (isChecked4)
            {
                rating = 4;
            }
            else if (isChecked5)
            {
                rating = 5;
            }
            return rating;
        }

        public List<string> GetQuestions(params string[] questions)
        {
            List<string> questionList = new List<string>();
            foreach (string question in questions)
            {
                questionList.Add(question);
            }
            return questionList;
        }

        public void ClearQuestionRatings(params bool[] ratings)
        {
            for (int i = 0; i < ratings.Length; i++)
            {
                ratings[i] = false;
            }
        }
    }
}
