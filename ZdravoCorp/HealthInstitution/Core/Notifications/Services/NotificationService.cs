using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Notifications.Model;
using ZdravoCorp.HealthInstitution.Core.Notifications.Repository;

namespace ZdravoCorp.HealthInstitution.Core.Notifications.Services
{
    public class NotificationService
    {

        private readonly INotificationRepository _repository;


        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public void AddNotification(Notification notification)
        {
            _repository.Add(notification);
        }

        public List<Notification> GetNotificationsForPatient(int id)
        {
            List<Notification> notificationList = _repository.Load();
            List<Notification> patientNotifications = new List<Notification>();
            foreach (Notification notification in notificationList)
            {
                if (notification.patientId == id) patientNotifications.Add(notification);
            }
            return patientNotifications;
        }

        public void RemoveNotificationsForPatient(int id)
        {
            List<Notification> notificationList = _repository.Load();
            foreach (Notification notification in notificationList)
            {
                if (notification.patientId == id) _repository.Remove(notification);
            }
        }
    }
}
