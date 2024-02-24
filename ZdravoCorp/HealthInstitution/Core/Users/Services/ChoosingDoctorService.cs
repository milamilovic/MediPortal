using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.CRUD;
using ZdravoCorp.HealthInstitution.GUI.PatientWindows;

namespace ZdravoCorp.HealthInstitution.Core.Users.Services
{
    public class ChoosingDoctorService
    {
        public static Doctor[] FindByName(string keyword)
        {
            List<Doctor> doctorsList = new List<Doctor>();
            if (keyword != "")
            {
                Doctor[] doctors = Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
                if (doctors != null)
                {
                    foreach (Doctor doctor in doctors)
                    {
                        if (doctor.Name.ToLower().Contains(keyword.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            doctorsList.Add(doctor);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Insert a keyword to find!", "Warning");
            }
            return doctorsList.ToArray();
        }

        public static Doctor[] FindBySurame(string keyword)
        {
            List<Doctor> doctorsList = new List<Doctor>();
            if (keyword != "")
            {
                Doctor[] doctors = Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
                if (doctors != null)
                {
                    foreach (Doctor doctor in doctors)
                    {
                        if (doctor.Surname.ToLower().Contains(keyword.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            doctorsList.Add(doctor);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Insert a keyword to find!", "Warning");
            }
            return doctorsList.ToArray();
        }

        public static Doctor[] FindBySpec(string[] specs)
        {
            List<Doctor> doctorsList = new List<Doctor>();
            if (specs.Length != 0)
            {
                Doctor[] doctors = Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
                if (doctors != null)
                {
                    foreach (Doctor doctor in doctors)
                    {
                        if (specs.Contains(doctor.Speciality.ToString()))
                        {
                            doctorsList.Add(doctor);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a specialization to find!", "Warning");
            }
            return doctorsList.ToArray();
        }

        public static string[] GetSpecializations(CheckBox generalMedSpec, CheckBox surgerySpec, CheckBox cardiologySpec)
        {
            List<string> specializations = new List<string>();
            bool genMedChecked = generalMedSpec.IsChecked ?? false;
            bool surgChecked = surgerySpec.IsChecked ?? false;
            bool cardChecked = cardiologySpec.IsChecked ?? false;
            if (genMedChecked)
            {
                specializations.Add(generalMedSpec.Content.ToString());
            }
            if (surgChecked)
            {
                specializations.Add(surgerySpec.Content.ToString());
            }
            if (cardChecked)
            {
                specializations.Add(cardiologySpec.Content.ToString());
            }
            return specializations.ToArray();
        }

        public static void RateDoctor(Doctor doctor, Patient patient)
        {
            DoctorSurveyView window = new DoctorSurveyView(doctor, patient);
            window.Top = 100;
            window.Left = 800;
            window.ShowDialog();
        }

        public static Doctor[] SortByName(Doctor[] doctors)
        {
            SortedDictionary<string, Doctor> dict = new SortedDictionary<string, Doctor>();
            if (doctors != null)
            {
                foreach (Doctor doctor in doctors)
                {
                    // sorting doctors by name
                    dict.Add(doctor.Name, doctor);
                }
            }
            else
            {
                MessageBox.Show("Doctors do not exist. \nSorry.");
            }
            return dict.Values.ToArray();
        }

        public static Doctor[] SortBySurname(Doctor[] doctors)
        {
            SortedDictionary<string, Doctor> dict = new SortedDictionary<string, Doctor>();
            if (doctors != null)
            {
                foreach (Doctor doctor in doctors)
                {
                    // sorting doctors by surname
                    dict.Add(doctor.Surname, doctor);
                }
            }
            else
            {
                MessageBox.Show("Doctors do not exist. \nSorry.");
            }
            return dict.Values.ToArray();
        }

        public static Doctor[] SortBySpeciality(Doctor[] doctors)
        {
            SortedDictionary<Doctor.DoctorsSpeciality, Doctor> dict = new SortedDictionary<Doctor.DoctorsSpeciality, Doctor>();
            if (doctors != null)
            {
                foreach (Doctor doctor in doctors)
                {
                    // sorting doctors by speciality
                    dict.Add(doctor.Speciality, doctor);
                }
            }
            else
            {
                MessageBox.Show("Doctors do not exist. \nSorry.");
            }
            return dict.Values.ToArray();
        }

        public static Doctor[] SortByRate(Doctor[] doctors)
        {
            SortedDictionary<double, Doctor> dict = new SortedDictionary<double, Doctor>();
            if (doctors != null)
            {
                foreach (Doctor doctor in doctors)
                {
                    // sorting doctors by rate
                    dict.Add(5 - doctor.AverageRate, doctor);
                }
            }
            else
            {
                MessageBox.Show("Doctors do not exist. \nSorry.");
            }
            return dict.Values.ToArray();
        }

        public static void CreateExamWithDoctor(Doctor doctor, Patient patient)
        {
            PatientExaminationCRUDWindow window = new PatientExaminationCRUDWindow(null, patient.Id);
            window.DoctorId = doctor.Id;
            window.doc1.IsEnabled = false;
            window.doc2.IsEnabled = false;
            window.doc3.IsEnabled = false;
            if (doctor.Id == 1)
            {
                window.doc1.IsChecked = true;
            }
            else if (doctor.Id == 2)
            {
                window.doc2.IsChecked = true;
            }
            else { window.doc3.IsChecked = true; }
            window.Top = 100;
            window.Left = 600;
            window.ShowDialog();
        }
    }
}
