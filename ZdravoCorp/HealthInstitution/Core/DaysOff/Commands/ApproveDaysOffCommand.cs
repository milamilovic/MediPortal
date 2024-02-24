using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Notifications.Model;
using ZdravoCorp.HealthInstitution.Core.Notifications.Repository;
using ZdravoCorp.HealthInstitution.Core.Notifications.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;
using ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.DaysOff.Commands
{
    public class ApproveDaysOffCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private DaysOfftReviewViewModel viewModel;

        public ApproveDaysOffCommand(DaysOfftReviewViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int requestId = viewModel.SelectedRequest.RequestId;
            var allDaysOff = DaysOffRepository.Deserialize();
            foreach (DaysOffRequest dayOff in allDaysOff)
            {
                if (dayOff.RequestId == requestId)
                {
                    dayOff.Approved = true;
                    DaysOffRepository.Serialize(allDaysOff);
                    CancelAppointments(dayOff);
                    break;
                }
            }
            MessageBox.Show("Days off request with id " + requestId + " is approved!");
            viewModel.InitializeRequests();
        }

        internal static void CancelAppointments(DaysOffRequest daysOff)
        {
            string fileName = "../../../Data/Examinations/Examinations.json";
            Examination[] examinations = Examination.LoadExaminations(fileName);
            List<Examination> updated = new List<Examination>();
            foreach (Examination examination in examinations)
            {
                bool deleted = false;
                if (examination.DoctorId == daysOff.DoctorId)
                {
                    TimeSlot daysOffTimeSlot = new TimeSlot(daysOff.StartDate.Date.ToString("dd.MM.yyyy."), "00:00:00", (daysOff.EndDate - daysOff.StartDate).ToString());
                    if (RoomSchedule.CheckOverlap(daysOffTimeSlot, examination.TimeSlot))
                    {
                        AddNotification(examination);
                        deleted = true;

                    }
                }
                if (!deleted) { updated.Add(examination); }
            }
            Examination.Save(updated.ToArray());
        }

        internal static void AddNotification(Examination examination)
        {
            string text = "Your examination on " + TimeSlot.GetStartTime(examination.TimeSlot).ToString() + " is canceled because the doctor is not avaliable at that time.";
            Notification notification = new Notification(text, examination.PatientId);
            NotificationService service = new NotificationService(new NotificationRepository());
            service.AddNotification(notification);
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
