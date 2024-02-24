using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Notifications.Model;

namespace ZdravoCorp.HealthInstitution.Core.Notifications.Repository
{
    public interface INotificationRepository
    {
        void Save(List<Notification> notifications);
        List<Notification> Load();
        void Add(Notification notification);
        void Remove(Notification notification);
    }
}
