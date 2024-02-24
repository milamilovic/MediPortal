using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository
{
    public interface IChatRepository
    {
        Chat[] LoadPersonal(string username);
        Chat[] Load();
        void Save(Chat[] chats);
    }
}
