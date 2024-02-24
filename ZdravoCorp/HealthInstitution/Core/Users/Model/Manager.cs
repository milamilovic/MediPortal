using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Users.Model
{
    public class Manager
    {
        public string Username;
        public string Password;
        public string Name;
        public string Email;
        public string Surname;
        public string Birthday;
        public string Image;

        public Manager(string username, string password, string name, string email, string birthday, string image)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Birthday = birthday;
            Image = image;
        }

        public static Manager FindManagerByUsername(string username)
        {
            var jsontext = File.ReadAllText("../../../Data/Users/managers.json");
            Manager[] users = JsonConvert.DeserializeObject<Manager[]>(jsontext)!;
            foreach (var user in users)
            {
                if (user.Username == username) { return user; }
            }
            return null;
        }
    }
}
