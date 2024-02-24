using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.Core.Schedules.Model.DoctorAvailability
{
    internal class DoctorWorkAvailability : IDoctorAvailability
    {
        public bool IsAvailable(Examination exam, int docId, TimeSlot timeSlot)
        {
            Doctor doctor = Doctor.Find(docId);
            List<TimeSlot> timeSlots = doctor.GetBusyTimeSlots();
            return Schedule.CheckOverlap(exam, timeSlot, timeSlots);
        }
    }
}
