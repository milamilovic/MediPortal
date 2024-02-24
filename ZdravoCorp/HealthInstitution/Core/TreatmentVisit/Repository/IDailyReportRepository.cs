using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Repository
{
    public interface IDailyReportRepository
    {
        DailyReport GetById(int id);
        void Add(DailyReport entity);
        public void Update(List<DailyReport> visits);
        void Delete(int id);
        List<DailyReport> GetAll();
    }
}
