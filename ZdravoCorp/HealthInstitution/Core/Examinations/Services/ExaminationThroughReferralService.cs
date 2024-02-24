using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.Referrals.Services;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Examinations.Services
{
    internal class ExaminationThroughReferralService
    {
        public static bool ScheduleExaminationThroughReferral(Patient patient)
        {
            ExaminationReferral referral = FindPatientsReferral(patient);
            if (referral == null) return false;

            ScheduleExamination(referral.DoctorId, patient);

            return true;
        }

        public static ExaminationReferral FindPatientsReferral(Patient patient)
        {
            ExaminationReferralRepository repository = new ExaminationReferralRepository();
            ExaminationReferral[] allReferrals = repository.LoadFile();
            foreach (ExaminationReferral referral in allReferrals)
            {
                if (referral.PatientId == patient.Id && !referral.WasUsed)
                {
                    referral.WasUsed = true;
                    repository.WriteFile(allReferrals);

                    return referral;
                }
            }
            return null;
        }

        public static void ScheduleExamination(int doctorId, Patient patient)
        {
            List<DateTime> freeStartTimes = GetEarliestFreeStartTimes(doctorId, patient);
            Doctor doctor = Doctor.Find(doctorId);

            for (int j = 0; j < freeStartTimes.Count(); j++)
            {
                string date = freeStartTimes[j].ToString("dd.MM.yyyy.");
                string time = freeStartTimes[j].ToString("HH:mm:ss");
                TimeSlot freeTimeSlot = new TimeSlot(date, time, "00:15:00");

                Examination exam = EmergencyExaminationService.CreateExamination(patient, doctor, freeTimeSlot, false);

                if (Schedule.IsFree(null, doctor.Id, freeTimeSlot, "doctor") && Schedule.IsFree(null, patient.Id, freeTimeSlot, "patient"))
                {
                    Examination.Create(exam.Id, true, doctor.Id, patient.Id, freeTimeSlot);
                    return;
                }
            }

        }

        public static List<DateTime> GetEarliestFreeStartTimes(int doctorId, Patient patient)
        {
            Doctor doctor = Doctor.Find(doctorId);
            List<DateTime> startTimes = new List<DateTime>();
            List<TimeSlot> busyTimeSlotsDoctor = doctor.GetBusyTimeSlots();

            DateTime earilest = DateTime.Now;
            earilest = earilest.AddDays(1);
            earilest = earilest.AddSeconds(10);

            bool isEarliestCovered = false;
            for (int i = 0; i < busyTimeSlotsDoctor.Count(); i++)
            {
                if (TimeSlot.GetStartTime(busyTimeSlotsDoctor[i]).CompareTo(earilest) <= 0 && earilest.CompareTo(TimeSlot.GetEndTime(busyTimeSlotsDoctor[i])) <= 0)
                {
                    isEarliestCovered = true;
                    break;
                }
            }
            if (!isEarliestCovered) { startTimes.Add(earilest); }

            for (int i = 0; i < busyTimeSlotsDoctor.Count(); i++)
            {
                if (earilest.CompareTo(TimeSlot.GetEndTime(busyTimeSlotsDoctor[i])) <= 0)
                {
                    startTimes.Add(TimeSlot.GetEndTime(busyTimeSlotsDoctor[i]));
                }
            }

            List<TimeSlot> busyTimeSlotsPatient = patient.GetBusyTimeSlots();
            for (int i = 0; i < busyTimeSlotsPatient.Count(); i++)
            {
                if (earilest.CompareTo(TimeSlot.GetEndTime(busyTimeSlotsPatient[i])) <= 0)
                {
                    startTimes.Add(TimeSlot.GetEndTime(busyTimeSlotsPatient[i]));
                }
            }

            return startTimes;
        }


    }
}
