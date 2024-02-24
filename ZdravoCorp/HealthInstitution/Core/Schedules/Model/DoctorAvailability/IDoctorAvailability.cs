using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;

namespace ZdravoCorp.HealthInstitution.Core.Schedules.Model.DoctorAvailability
{
    internal interface IDoctorAvailability
    {
        bool IsAvailable(Examination exam, int docId, TimeSlot timeSlot);
    }
}
