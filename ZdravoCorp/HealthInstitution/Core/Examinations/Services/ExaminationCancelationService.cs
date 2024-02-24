using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Examinations.Services
{
    public class ExaminationCancelationService
    {
        public static void CancelExaminationsInTimeSlot(int doctorId, TimeSlot period)
        {
            List<Examination> doctorsExaminations = Examination.GetDoctorExaminations(doctorId);
            HashSet<int> takenIds = new HashSet<int>(doctorsExaminations.Select(examination => examination.Id));

            string fileName = "../../../Data/Examinations/Examinations.json";
            Examination[] allExaminations = Examination.LoadExaminations(fileName);
            Examination[] examinations = FindAdequate(takenIds, ref allExaminations, period);
            Examination.Save(examinations);
        }

        public static Examination[] FindAdequate(HashSet<int> takenIds, ref Examination[] allExaminations, TimeSlot period)
        {
            List<Examination> adequateExaminations = new List<Examination>();

            foreach (Examination examination in allExaminations)
            {
                if (!takenIds.Contains(examination.Id))
                {
                    if (!CheckOverlap(examination.TimeSlot, period))
                    {
                        adequateExaminations.Add(examination);
                    }
                }
            }
            return adequateExaminations.ToArray();
        }

        public static bool CheckOverlap(TimeSlot exitstingAppointment, TimeSlot daysOff)
        {
            DateTime startTimeExisting = TimeSlot.GetStartTime(exitstingAppointment);
            DateTime endTimeExisting = TimeSlot.GetEndTime(exitstingAppointment);
            DateTime startTimeDaysOff = TimeSlot.GetStartTime(daysOff);
            DateTime endTimeDaysOff = TimeSlot.GetEndTime(daysOff);

            bool overlap = startTimeExisting < endTimeDaysOff && startTimeDaysOff < endTimeExisting;

            return overlap;
        }
    }
}
