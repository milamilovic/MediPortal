using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Users.Repository
{
    public interface ILoginRepository
    {
        Profile[] LoadUsers();
        void SaveUsers(Profile[] users);
        void Add(Profile user);
    }
}
