using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.Core.Schedules.Model.DoctorAvailability
{
    internal class DoctorVacationAvailability:IDoctorAvailability
    {
        public bool IsAvailable(Examination exam, int docId, TimeSlot timeSlot)
        {
            List<DaysOffRequest> daysOff = DaysOffRepository.Deserialize();
            DateTime date = TimeSlot.GetStartTime(timeSlot);
            foreach (DaysOffRequest dayOff in daysOff)
            {
                if (dayOff.DoctorId == docId && dayOff.Approved == true)
                {
                    if (date.Date >= dayOff.StartDate.Date &&
                        date.Date <= dayOff.EndDate.Date)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
