using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Notifications.Model
{
    public class Notification
    {
        public string notification { get; set; }
        public int patientId { get; set; }


        public Notification(string notification, int patientId)
        {
            this.notification = notification;
            this.patientId = patientId;
        }
    }
}
