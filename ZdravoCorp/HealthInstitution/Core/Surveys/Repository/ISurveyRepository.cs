using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Repository
{
    public interface ISurveyRepository
    {
        Survey GetById(int id);
        void Add(Survey entity);
        public void Update(List<Survey> surveys);
        void Delete(int id);
        List<Survey> GetAll();
        List<int> GetSurveyIds();
        int GetNextSurveyId();
    }
}
