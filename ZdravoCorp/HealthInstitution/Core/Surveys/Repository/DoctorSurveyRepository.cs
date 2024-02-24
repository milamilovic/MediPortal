using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Surveys;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Repository
{
    public class DoctorSurveyRepository : IDoctorSurveyRepository
    {
        public void Add(DoctorSurvey entity)
        {
            List<DoctorSurvey> DoctorSurveys = GetAll();
            DoctorSurveys.Add(entity);
            Update(DoctorSurveys);
        }

        public void Delete(int id)
        {
            List<DoctorSurvey> doctorSurveys = GetAll();
            foreach (DoctorSurvey doctorSurvey in doctorSurveys)
            {
                if (doctorSurvey.Id == id) doctorSurveys.Remove(doctorSurvey);
                Update(doctorSurveys);
                return;
            }
        }

        public List<DoctorSurvey> GetAll()
        {
            return Load();
        }

        public DoctorSurvey GetById(int id)
        {
            List<DoctorSurvey> DoctorSurveys = GetAll();
            foreach (DoctorSurvey DoctorSurvey in DoctorSurveys)
            {
                if (DoctorSurvey.Id == id) return DoctorSurvey;
            }
            return null;
        }

        public void Update(List<DoctorSurvey> DoctorSurveys)
        {
            Save(DoctorSurveys);
        }

        public void Save(List<DoctorSurvey> DoctorSurveys)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string DoctorSurveysJson = System.Text.Json.JsonSerializer.Serialize(DoctorSurveys, options);
            File.WriteAllText("../../../Data/Surveys/DoctorSurvey.json", DoctorSurveysJson);
        }

        public List<DoctorSurvey> Load()
        {
            var jsontext = File.ReadAllText("../../../Data/Surveys/DoctorSurvey.json");
            List<DoctorSurvey> allDoctorSurveys = JsonConvert.DeserializeObject<List<DoctorSurvey>>(jsontext)!;
            return allDoctorSurveys;
        }

        public List<int> GetDoctorSurveyIds()
        {
            List<DoctorSurvey> surveys = Load();
            if (surveys.Count != 0)
            {
                return surveys.Select(survey => survey.Id).ToList();
            }
            return null;
        }

        public int GetNextDoctorSurveyId()
        {
            Random rnd = new Random();
            int nextId = rnd.Next();
            List<int> surveyIds = GetDoctorSurveyIds();
            if (surveyIds != null)
            {
                while (surveyIds.Contains(nextId))
                {
                    nextId = rnd.Next();
                }
                return nextId;
            }
            return nextId;
        }
    }
}
