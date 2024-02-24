using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.DaysOff;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        public void Add(Survey entity)
        {
            List<Survey> surveys = GetAll();
            surveys.Add(entity);
            Update(surveys);
        }

        public void Delete(int id)
        {
            List<Survey> surveys = GetAll();
            foreach (Survey survey in surveys)
            {
                if (survey.Id == id) surveys.Remove(survey);
                Update(surveys);
                return;
            }
        }

        public List<Survey> GetAll()
        {
            return Load();
        }

        public Survey GetById(int id)
        {
            List<Survey> surveys = GetAll();
            foreach (Survey survey in surveys)
            {
                if (survey.Id == id) return survey;
            }
            return null;
        }

        public void Update(List<Survey> surveys)
        {
            Save(surveys);
        }

        private void Save(List<Survey> surveys)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string surveysJson = System.Text.Json.JsonSerializer.Serialize(surveys, options);
            File.WriteAllText("../../../Data/Surveys/HospitalSurvey.json", surveysJson);
        }

        private List<Survey> Load()
        {
            var jsontext = File.ReadAllText("../../../Data/Surveys/HospitalSurvey.json");
            List<Survey> allSurveys = JsonConvert.DeserializeObject<List<Survey>>(jsontext)!;
            return allSurveys;
        }

        public List<int> GetSurveyIds()
        {
            List<Survey> surveys = Load();
            if (surveys.Count != 0)
            {
                return surveys.Select(survey => survey.Id).ToList();
            }
            return null;
        }

        public int GetNextSurveyId()
        {
            Random rnd = new Random();
            int nextId = rnd.Next();
            List<int> surveyIds = GetSurveyIds();
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
