using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZdravoCorp.HealthInstitution.Core.Users.Model
{
    public class Nurse
    {
        public string Username;
        public string Password;
        public string Name;
        public string Email;
        public string Surname;
        public string Birthday;

        public Nurse() { }
        public Nurse(string username, string password)
        {
            Nurse nurse = FindNurseByUsername(username);
            Username = nurse.Username;
            Password = nurse.Password;
            Name = nurse.Name;
            Surname = nurse.Surname;
            Email = nurse.Email;
            Birthday = nurse.Birthday;
        }
        public Nurse(string username, string password, string name, string email, string surname, string birthday)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;
        }

        public static Nurse FindNurseByUsername(string username)
        {
            Nurse[] nurses = LoadNurses();
            foreach (Nurse nurse in nurses)
            {
                if (nurse.Username == username) return nurse;
            }
            return null;
        }
        public static Nurse[] LoadNurses()
        {
            var nursesJson = File.ReadAllText("../../../Data/Users/nurses.json");
            Nurse[] allNurses = JsonConvert.DeserializeObject<Nurse[]>(nursesJson)!;
            return allNurses;
        }
    }
}
