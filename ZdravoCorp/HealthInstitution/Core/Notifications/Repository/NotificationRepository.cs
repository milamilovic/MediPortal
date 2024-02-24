using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Notifications.Model;
using ZdravoCorp.HealthInstitution.DaysOff;

namespace ZdravoCorp.HealthInstitution.Core.Notifications.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public void Add(Notification notification)
        {
            List<Notification> notifications = Load();
            notifications.Add(notification);
            Save(notifications);

        }

        public List<Notification> Load()
        {
            string fileName = "../../../Data/Notifications/PatientNotifications.json";
            var jsontext = File.ReadAllText(fileName);
            List<Notification> notifications = JsonConvert.DeserializeObject<List<Notification>>(jsontext)!;
            return notifications;
        }

        public void Remove(Notification notification)
        {
            List<Notification> notifications = Load();
            foreach (Notification n in notifications)
            {
                if (n.notification.Equals(notification.notification) && n.patientId == notification.patientId)
                {
                    notifications.Remove(n);
                    Save(notifications);
                    return;
                }
            }
        }

        public void Save(List<Notification> notifications)
        {
            string fileName = "../../../Data/Notifications/PatientNotifications.json";
            dynamic text = JsonConvert.SerializeObject(notifications, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }
    }
}
