using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.Core.DaysOff.Model
{
    internal class DaysOffRequest
    {
        public int RequestId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Cause { get; set; }
        public bool Approved { get; set; }

        public DaysOffRequest(int requestId, int doctorId, DateTime startDate, DateTime endDate, string cause)
        {
            RequestId = requestId;
            StartDate = startDate;
            EndDate = endDate;
            Cause = cause;
            Approved = false;
            DoctorId = doctorId;
        }
        public static int GetNewId()
        {
            List<DaysOffRequest> allDaysOff = DaysOffRepository.Deserialize();
            int id = 1;
            bool taken = false;
            while (true)
            {
                for (int i = 0; i < allDaysOff.Count; i++)
                {
                    if (allDaysOff[i].RequestId == id) { taken = true; break; }
                }
                if (!taken)
                {
                    return id;
                }
                taken = false;
                id++;
            }
        }
    }
}

