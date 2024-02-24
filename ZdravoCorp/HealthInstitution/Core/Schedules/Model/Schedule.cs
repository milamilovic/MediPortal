using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model.DoctorAvailability;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Schedules.Model
{
    internal class Schedule
    {
        public static bool CheckOverlap(Examination exam, TimeSlot wantedTimeSlot, List<TimeSlot> allTimeSlots)
        {
            //if (allTimeSlots.Contains(wantedTimeSlot))
            //{
            //    return false;
            //}
            DateTime wantedStartTime = TimeSlot.GetStartTime(wantedTimeSlot);
            DateTime wantedEndTime = TimeSlot.GetEndTime(wantedTimeSlot);
            foreach (TimeSlot timeSlot in allTimeSlots)
            {
                if (exam != null)
                {
                    if (exam.TimeSlot.Date == timeSlot.Date && exam.TimeSlot.StartTime == timeSlot.StartTime && timeSlot.Duration == timeSlot.Duration)
                        continue;
                }

                DateTime startTime = TimeSlot.GetStartTime(timeSlot);
                DateTime endTime = TimeSlot.GetEndTime(timeSlot);
                if (wantedStartTime.CompareTo(startTime) <= 0 && wantedEndTime.CompareTo(endTime) >= 0)
                {
                    return false;
                }
                else if (wantedStartTime.CompareTo(startTime) >= 0 && wantedEndTime.CompareTo(endTime) <= 0)
                {
                    return false;
                }
                else if (wantedStartTime.CompareTo(startTime) <= 0 && wantedEndTime.CompareTo(endTime) <= 0 &&
                    wantedEndTime.CompareTo(startTime) > 0)
                {
                    return false;
                }
                else if (wantedStartTime.CompareTo(startTime) >= 0 && wantedEndTime.CompareTo(endTime) >= 0 &&
                    wantedStartTime.CompareTo(endTime) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsFree(Examination exam, int roleId, TimeSlot timeSlot, string role)
        {
            if (role == "doctor")
            {
                IDoctorAvailability doctorVacationAvailability = new DoctorVacationAvailability();
                IDoctorAvailability doctorWorkAvailability = new DoctorWorkAvailability();
                if (doctorVacationAvailability.IsAvailable(exam, roleId, timeSlot))
                {
                    if (doctorWorkAvailability.IsAvailable(exam, roleId, timeSlot))
                    {
                        return true;
                    }
                }

            }
            else if (role == "patient")
            {
                Patient[] patients = Patient.LoadPatients("../../../Data/PatientInfo/PatientData.json");
                foreach (Patient patient in patients)
                {
                    if (patient.GetId() == roleId)
                    {
                        List<TimeSlot> timeSlots = patient.GetBusyTimeSlots();
                        return CheckOverlap(exam, timeSlot, timeSlots);
                    }
                }
            }
            return false;
        }
    }


}
