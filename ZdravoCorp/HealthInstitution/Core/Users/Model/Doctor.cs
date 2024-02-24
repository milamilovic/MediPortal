using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Users.Model
{
    public class Doctor
    {
        public enum DoctorsSpeciality { Cardiology, GeneralMedicine, Surgery };

        [JsonProperty]
        public string Username { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public DoctorsSpeciality Speciality { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Surname { get; set; }
        [JsonProperty]
        public List<int> Rates;
        [JsonProperty]
        public double AverageRate { get; set; }
        [JsonProperty]
        public List<TimeSlot> BusyTimeSlots;

        public Doctor(string username, string password)    //constructor for login
        {
            Username = username;
            Password = password;
        }
        [JsonConstructor]
        public Doctor(string username, string password, int id, DoctorsSpeciality speciality, string name, string surname, List<int> rates, double averageRate, List<TimeSlot> busyTimeSlots)
        {
            Username = username;
            Password = password;
            Id = id;
            Speciality = speciality;
            Name = name;
            Surname = surname;
            Rates = rates;
            AverageRate = averageRate;
            BusyTimeSlots = busyTimeSlots;
        }
        public static int FindByUsername(string username)
        {
            Doctor[] doctors = LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Username == username)
                {
                    return doctor.Id;
                }
            }
            return 0;
        }
        public static Doctor Find(int doctorId)
        {
            Doctor[] doctors = LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
            foreach (Doctor doctor in doctors)
            {
                if (doctor.GetId() == doctorId)
                {
                    return doctor;
                }
            }
            return null;

        }
        public static List<Doctor> FindSpecialisedDoctors(DoctorsSpeciality speciality)
        {
            Doctor[] allDoctors = LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
            List<Doctor> specialisedDoctors = new List<Doctor>();
            foreach (Doctor d in allDoctors)
            {
                if (d.Speciality == speciality)
                {
                    specialisedDoctors.Add(d);
                }
            }
            return specialisedDoctors;

        }

        public int GetId()
        {
            return Id;
        }
        public List<TimeSlot> GetBusyTimeSlots()
        {
            return BusyTimeSlots;
        }
        public void SetBusyTimeSlots(List<TimeSlot> newList)
        {
            BusyTimeSlots = newList;
        }
        public void SetData(Doctor doctor)
        {
            Username = doctor.Username;
            Password = doctor.Password;
            Id = doctor.Id;
            Name = doctor.Name;
            Surname = doctor.Surname;
            Rates = doctor.Rates;
            AverageRate = doctor.AverageRate;
            BusyTimeSlots = doctor.BusyTimeSlots;
        }
        public static Doctor[] LoadDoctors(string filename)
        {
            var jsontext = File.ReadAllText(filename);
            Doctor[] doctors = JsonConvert.DeserializeObject<Doctor[]>(jsontext)!;
            return doctors;
        }

        public static void SetAverageRate(Doctor doctor)
        {
            List<int> rates = doctor.Rates;
            int sum = 0;
            foreach (int rate in rates)
            {
                sum += rate;
            }
            if (rates.Count > 0)
            {
                decimal average = Math.Round(decimal.Divide(sum, rates.Count), 2);
                doctor.AverageRate = (double)average;
            }
            else { doctor.AverageRate = 0.0; }
            WriteFile("../../../Data/DoctorInfo/DoctorData.json", doctor, doctor);
        }

        public static void WriteFile(string filename, Doctor oldDoctor, Doctor newDoctor)
        {
            Doctor[] doctors = LoadDoctors(filename);
            foreach (Doctor doctor in doctors)
            {
                if (doctor.GetId() == oldDoctor.GetId())
                {
                    doctor.SetData(oldDoctor);

                }
                else if (doctor.GetId() == newDoctor.GetId())
                {
                    doctor.SetData(newDoctor);
                }
            }
            File.WriteAllText(@"../../../Data/DoctorInfo/DoctorData.json", JsonConvert.SerializeObject(doctors, Formatting.Indented));

        }
        internal List<TimeSlot> GetBusyTimeSlotsForDate(DateTime date)
        {
            List<TimeSlot> timeSlotsForDate = new List<TimeSlot>();
            foreach (TimeSlot timeSlot in BusyTimeSlots)
            {
                if (timeSlot.Date == date.ToString("dd.MM.yyyy."))   //busy time slot is on the check up date
                {
                    timeSlotsForDate.Add(timeSlot);
                }
            }
            return timeSlotsForDate;
        }

    }
}
