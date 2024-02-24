using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.PatientWindows;

namespace ZdravoCorp.HealthInstitution.Core.Reminders.Services
{
    internal class NotificationService
    {
        public static OtherReminder[] GetPatientOtherReminders(Patient patient)
        {
            OtherReminder[] reminders = OtherReminder.Load();
            List<OtherReminder> remindersList = new List<OtherReminder>();
            foreach (OtherReminder reminder in reminders)
            {
                if (reminder.PatientId == patient.Id)
                {
                    remindersList.Add(reminder);
                }
            }
            return remindersList.ToArray();
        }

        public static MedicineReminder[] GetPatientMedReminders(Patient patient)
        {
            MedicineReminder[] reminders = MedicineReminder.Load();
            List<MedicineReminder> remindersList = new List<MedicineReminder>();
            foreach (MedicineReminder reminder in reminders)
            {
                if (reminder.prescription.PatientId == patient.Id)
                {
                    remindersList.Add(reminder);
                }
            }
            return remindersList.ToArray();
        }

        public static OtherReminder[] GetPatientOtherRemindersForDays(Patient patient)
        {
            OtherReminder[] reminders = OtherReminder.Load();
            List<OtherReminder> remindersList = new List<OtherReminder>();
            foreach (OtherReminder reminder in reminders)
            {
                for (int i = 0; i < reminder.Days; i++)
                {
                    DateTime date = DateTime.ParseExact(reminder.StartDate, "dd.MM.yyyy.", null);
                    DateTime nextDate = date.AddDays(i);
                    if (reminder.PatientId == patient.Id &&
                        nextDate.ToString("dd.MM.yyyy.").Equals(DateTime.Now.Date.ToString("dd.MM.yyyy.")))
                    {
                        remindersList.Add(reminder);
                    }
                }
            }
            return remindersList.ToArray();
        }

        public static DateTime[] SetDate(Prescription prescription)
        {
            DateTime dateTime = DateTime.Parse(prescription.Date);
            List<DateTime> dtList = new List<DateTime>();
            for (int i = 0; i < prescription.Days; i++)
            {
                dtList.Add(dateTime.AddDays(i));
            }
            return dtList.ToArray();
        }

        public static DateTime[] SetDateForOtherReminder(OtherReminder reminder)
        {
            DateTime dateTime = DateTime.Parse(reminder.StartDate);
            List<DateTime> dtList = new List<DateTime>();
            for (int i = 0; i < reminder.Days; i++)
            {
                dtList.Add(dateTime.AddDays(i));
            }
            return dtList.ToArray();
        }

        public static DateTime[] SetTime(DateTime dt, int times)
        {
            List<DateTime> dtList = new List<DateTime>();
            for (int i = 0; i < times; i++)
            {
                dtList.Add(dt.AddHours(i * (24 / times)));
            }
            return dtList.ToArray();
        }

        public static bool IsTimeForMedicine(Prescription prescription, int timer)
        {
            int times = int.Parse(prescription.Schedule.Split("x")[1]);
            DateTime[] dates = SetDate(prescription);
            // date is each day from prescription for which we set timings for medicine
            DateTime date = DateTime.Now;
            foreach (DateTime dt in dates)
            {
                if (dt.Date.Equals(DateTime.Now.Date))
                {
                    date = dt;
                }
            }
            DateTime[] timings = SetTime(date.AddMinutes(-timer), times);
            foreach (DateTime t in timings)
            {
                if (DateTime.Now.Date.Equals(t.Date) && DateTime.Now.Hour.Equals(t.Hour)
                    && DateTime.Now.Minute.Equals(t.Minute))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTimeForNotification(OtherReminder reminder, int timer)
        {
            DateTime[] dates = SetDateForOtherReminder(reminder);
            DateTime date = DateTime.Now;
            foreach (DateTime dt in dates)
            {
                if (dt.Date.Equals(DateTime.Now.Date))
                {
                    date = dt;
                }
            }
            DateTime[] timings = SetTime(date.AddMinutes(-timer), reminder.times);
            foreach (DateTime t in timings)
            {
                if (DateTime.Now.Date.Equals(t.Date) && DateTime.Now.Hour.Equals(t.Hour)
                    && DateTime.Now.Minute.Equals(t.Minute))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateOtherReminder(string name, string description, string startDate, int days, int times, int patientId, int timer)
        {
            OtherReminder newReminder = new OtherReminder(name, description, startDate, days, times, patientId, timer);
            OtherReminder[] reminders = OtherReminder.Load();
            List<OtherReminder> remindersList = new List<OtherReminder>();
            foreach (OtherReminder reminder in reminders)
            {
                remindersList.Add(reminder);
            }
            remindersList.Add(newReminder);
            OtherReminder.Save(remindersList.ToArray());
            MessageBox.Show("Reminder successfully created!", "Confirmation");
        }

        public static void UpdateOtherReminder(OtherReminder oldReminder, string name, string description, string startDate, int days, int times, int patientId, int timer)
        {
            OtherReminder[] reminders = OtherReminder.Load();
            List<OtherReminder> remindersList = new List<OtherReminder>();
            foreach (OtherReminder reminder in reminders)
            {
                if (reminder.PatientId != oldReminder.PatientId ||
                reminder.StartDate != oldReminder.StartDate ||
                reminder.Times != oldReminder.Times ||
                reminder.Name != oldReminder.Name ||
                reminder.Description != oldReminder.Description ||
                reminder.Days != oldReminder.Days ||
                reminder.Timer != oldReminder.Timer)
                {
                    remindersList.Add(reminder);
                }
            }
            remindersList.Remove(oldReminder);
            OtherReminder newReminder = OtherReminder.SetOtherReminder(oldReminder, name, description, startDate, days, times, patientId, timer);
            remindersList.Add(newReminder);
            OtherReminder.Save(remindersList.ToArray());
            MessageBox.Show("Reminder successfully updated!", "Confirmation");
        }

        public static void DeleteOtherReminder(OtherReminder selectedReminder)
        {
            OtherReminder[] reminders = OtherReminder.Load();
            List<OtherReminder> remindersList = new List<OtherReminder>();
            foreach (OtherReminder reminder in reminders)
            {
                if (reminder.PatientId != selectedReminder.PatientId ||
                reminder.StartDate != selectedReminder.StartDate ||
                reminder.Times != selectedReminder.Times ||
                reminder.Name != selectedReminder.Name ||
                reminder.Description != selectedReminder.Description ||
                reminder.Days != selectedReminder.Days ||
                reminder.Timer != selectedReminder.Timer)
                {
                    remindersList.Add(reminder);
                }
            }
            remindersList.Remove(selectedReminder);
            OtherReminder.Save(remindersList.ToArray());
        }

        public static void SaveMedicineReminderTimer(Prescription prescription, int timer)
        {
            MedicineReminder[] reminders = MedicineReminder.Load();
            foreach (MedicineReminder reminder in reminders)
            {
                if (reminder.prescription.PatientId == prescription.PatientId &&
                    reminder.prescription.Date == prescription.Date &&
                    reminder.prescription.Schedule == prescription.Schedule &&
                    reminder.prescription.Medicine.Name == prescription.Medicine.Name &&
                    reminder.prescription.Meals == prescription.Meals)
                {
                    reminder.timer = timer;
                }
            }
            MedicineReminder.Save(reminders);
            MessageBox.Show("Timer for reminder successfully saved!", "Confirmation");
        }

        public static void DeleteMedicineReminder(MedicineReminder selectedReminder)
        {
            MedicineReminder[] reminders = MedicineReminder.Load();
            List<MedicineReminder> remindersList = new List<MedicineReminder>();
            foreach (MedicineReminder reminder in reminders)
            {
                if (reminder.prescription.PatientId != selectedReminder.prescription.PatientId ||
                reminder.prescription.Date != selectedReminder.prescription.Date ||
                reminder.prescription.Schedule != selectedReminder.prescription.Schedule ||
                reminder.prescription.Medicine.Name != selectedReminder.prescription.Medicine.Name ||
                reminder.prescription.Meals != selectedReminder.prescription.Meals)
                {
                    remindersList.Add(reminder);
                }
            }
            selectedReminder.Timer = 0;
            remindersList.Add(selectedReminder);
            MedicineReminder.Save(remindersList.ToArray());
            MessageBox.Show("Reminder successfully deleted!", "Confirmation");
        }
    }
}
