using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Notifications.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Users.Repository
{
    public class LoginRepository : ILoginRepository
    {
        public void SaveUsers(Profile[] users)
        {
            string fileName = "../../../Data/Login/LoginData.json";
            dynamic text = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        public Profile[] LoadUsers()
        {
            string fileName = "../../../Data/Login/LoginData.json";
            var jsontext = File.ReadAllText(fileName);
            Profile[] users = JsonConvert.DeserializeObject<Profile[]>(jsontext)!;
            return users;
        }

        public void Add(Profile user)
        {
            Profile[] users = LoadUsers();
            users.Append(user);
            SaveUsers(users);
        }
    }
}
