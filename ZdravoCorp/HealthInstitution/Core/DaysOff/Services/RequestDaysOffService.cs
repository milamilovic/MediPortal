using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.Core.DaysOff.Services
{
    internal class RequestDaysOffService
    {
        internal static bool CheckDates(DateTime startDate, DateTime endDate)
        {
            if (startDate < endDate)
            {
                if (startDate >= DateTime.Now.AddDays(2))
                {
                    return true;
                }
            }
            return false;
        }

        internal static void SendRequest(DateTime startDate, DateTime endDate, string reason, int docId)
        {
            DaysOffRequest dayOff = new DaysOffRequest(DaysOffRequest.GetNewId(),
                docId, startDate, endDate.Date, reason);
            DaysOffRepository.AddDayOffRequest(dayOff);
        }
    }
}
