using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Repository
{
    internal class DailyReportRepository : IDailyReportRepository
    {
        public void Add(DailyReport entity)
        {
            List<DailyReport> visits = GetAll();
            visits.Add(entity);
            Update(visits);
        }

        public void Delete(int id)
        {
            List<DailyReport> visits = GetAll();
            foreach (DailyReport visit in visits)
            {
                if (visit.VisitId == id) visits.Remove(visit);
                Update(visits);
                return;
            }
        }

        public List<DailyReport> GetAll()
        {
            var jsontext = File.ReadAllText("../../../Data/DailyReports/DailyReports.json");
            List<DailyReport> allVisits = JsonConvert.DeserializeObject<List<DailyReport>>(jsontext)!;
            return allVisits;
        }

        public DailyReport GetById(int id)
        {
            List<DailyReport> visits = GetAll();
            foreach (DailyReport visit in visits)
            {
                if (visit.VisitId == id)
                    return visit;
            }
            return null;
        }

        public void Update(List<DailyReport> visits)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string visitsJson = System.Text.Json.JsonSerializer.Serialize(visits, options);
            File.WriteAllText("../../../Data/DailyReports/DailyReports.json", visitsJson);
        }
    }
}
