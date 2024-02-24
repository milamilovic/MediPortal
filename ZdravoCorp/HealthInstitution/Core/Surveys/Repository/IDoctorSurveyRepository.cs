using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Repository
{
    public interface IDoctorSurveyRepository
    {
        void Add(DoctorSurvey entity);
        void Delete(int id);
        List<DoctorSurvey> GetAll();
        DoctorSurvey GetById(int id);
        void Update(List<DoctorSurvey> DoctorSurveys);
        void Save(List<DoctorSurvey> DoctorSurveys);
        List<DoctorSurvey> Load();
        List<int> GetDoctorSurveyIds();
        int GetNextDoctorSurveyId();
    }
}
