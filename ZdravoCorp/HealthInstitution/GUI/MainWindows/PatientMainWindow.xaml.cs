using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZdravoCorp.HealthInstitution.GUI.CRUD;
using System.Threading;
using ZdravoCorp.HealthInstitution.GUI.PatientWindows;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Anamneses.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Services;
using ZdravoCorp.HealthInstitution.Core.Surveys.Services;
using ZdravoCorp.HealthInstitution.GUI.Surveys.View;

namespace ZdravoCorp.HealthInstitution.GUI
{
    public partial class PatientMainWindow : Window
    {
        Thread medicineThread;
        public Patient patient;
        public static ObservableCollection<Examination> Examinations { get; set; }
        public Examination SelectedExamination { get; set; }
        public Anamnesis SelectedAnamnesis { get; set; }
        public Anamnesis[] GlobalAnamneses;
        public Doctor SelectedDoctor { get; set; }
        public Doctor[] ShownDoctors;
        public PatientMainWindow(Patient patient)
        {
            medicineThread = new Thread(new ThreadStart(CheckForNotification));
            medicineThread.IsBackground = true;
            medicineThread.Start();
            this.patient = patient;
            InitializeComponent();
            InitializeProfile(patient);
            DataContext = this;
            InitializeExaminationTable(patient);
            InitializeMedicalRecord(patient);
            InitializeAnamnesesTable(GetData(patient));
            InitializeDoctorsTable(Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json"));
        }

        public void InitializeAnamnesesTable(Anamnesis[] anamneses)
        {
            dataGrid.ItemsSource = anamneses;
        }

        public void InitializeDoctorsTable(Doctor[] doctors)
        {
            ShownDoctors = doctors;
            dataGridDoctors.ItemsSource = doctors;
        }

        public void InitializeMedicalRecord(Patient patient)
        {
            nameBox.Text = patient.MedicalRecord.Name;
            surnameBox.Text = patient.MedicalRecord.Surname;
            birthdayBox.Text = patient.MedicalRecord.Birthday;
            heightBox.Text = patient.MedicalRecord.Height;
            weightBox.Text = patient.MedicalRecord.Weight;
            medicalHistoryBox.Text = patient.MedicalRecord.MedicalHistory;
            nameBox.IsEnabled = false;
            surnameBox.IsEnabled = false;
            birthdayBox.IsEnabled = false;
            heightBox.IsEnabled = false;
            weightBox.IsEnabled = false;
            medicalHistoryBox.IsEnabled = false;
        }

        public static void InitializeExaminationTable(Patient patient)
        {
            Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
            List<Examination> patientExaminationsList = new List<Examination>();
            foreach (Examination ex in examinations)
            {
                if (ex.PatientId == patient.Id && ex.State != "deleted")
                {
                    patientExaminationsList.Add(ex);
                }
            }
            Examination[] patientExaminations = patientExaminationsList.ToArray();
            Examinations = new ObservableCollection<Examination>(patientExaminations);
        }

        public static void UpdateExaminationsCollection(Patient patient)
        {
            Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
            Examinations.Clear();
            foreach (Examination ex in examinations)
            {
                if (ex.PatientId == patient.Id && ex.State != "deleted")
                {
                    Examinations.Add(ex);
                }
            }
        }

        public void LogOutClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public void InitializeProfile(Patient patient)
        {
            Profile.Background = new ImageBrush(new BitmapImage(new Uri("../../../Data/Images/Patient/patient.png", UriKind.Relative)));
            name.Content += "  " + patient.MedicalRecord.Name;
            surname.Content += "  " + patient.MedicalRecord.Surname;
            username.Content += "  " + patient.Username;
            password.Content += "  " + patient.Password;
            birthday.Content += "  " + patient.MedicalRecord.Birthday;
            height.Content += "  " + patient.MedicalRecord.Height + " cm";
            weight.Content += "  " + patient.MedicalRecord.Weight + " kg";
            id.Content += "  " + patient.Id.ToString();
            role.Content += "  " + "Patient";
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            PatientExaminationCRUDWindow window = new PatientExaminationCRUDWindow(null, patient.Id);
            window.ShowDialog();
            UpdateExaminationsCollection(patient);
        }

        private void RecommendExaminationButtonClick(object sender, RoutedEventArgs e)
        {
            RecommendExaminationWindow window = new RecommendExaminationWindow(patient);
            window.ShowDialog();
            UpdateExaminationsCollection(patient);
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedExamination != null)
            {
                Examination selectedExam = SelectedExamination;
                PatientExaminationCRUDWindow window = new PatientExaminationCRUDWindow(selectedExam, patient.Id);
                window.ShowDialog();
                UpdateExaminationsCollection(patient);
            }
            else
            {
                MessageBox.Show("Select an examination to update", "Warning");
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedExamination != null)
            {
                Examination selectedExam = SelectedExamination;
                try
                {
                    Examination.Delete(selectedExam.Id);
                    MessageBox.Show("Examination deleted!", "Confirmation");
                    UpdateExaminationsCollection(patient);
                }
                catch
                {
                    MessageBox.Show("Invalid input type", "Warning");
                }
            }
            else
            {
                MessageBox.Show("Enter the examination id!", "Warning");
            }
        }

        private void ShowExamsButtonClick(object sender, RoutedEventArgs e)
        {
            PatientExaminationHistoryWindow window = new PatientExaminationHistoryWindow(patient);
            window.ShowDialog();
        }

        private Anamnesis[] GetData(Patient patient)
        {
            Anamnesis[] allAnamneses = Anamnesis.LoadAnamnesis();
            List<Anamnesis> anamnesesList = new List<Anamnesis>();
            if (allAnamneses != null)
            {
                foreach (Anamnesis anamnesis in allAnamneses)
                {
                    if (anamnesis.PatientId == patient.Id)
                    {
                        anamnesesList.Add(anamnesis);
                    }
                }
                return anamnesesList.ToArray();
            }
            return null;
        }

        private void SortByDateChecked(object sender, RoutedEventArgs e)
        {
            Anamnesis[] allAnamneses = GetData(patient);
            if (allAnamneses != null)
            {
                SortedDictionary<DateTime, Anamnesis> dict = new SortedDictionary<DateTime, Anamnesis>();
                foreach (Anamnesis anamnesis in allAnamneses)
                {
                    Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
                    TimeSlot ts = Examination.FindExamination(anamnesis.ExaminationId, examinations).TimeSlot;
                    DateTime examinationDateTime = DateTime.ParseExact(ts.Date + " " + ts.StartTime, "dd.MM.yyyy. HH:mm:ss", null);
                    dict.Add(examinationDateTime, anamnesis);
                }
                GlobalAnamneses = dict.Values.ToArray();
                InitializeAnamnesesTable(GlobalAnamneses);
            }
            else
            {
                MessageBox.Show("Anamneses do not exist. \nSorry.");
            }
        }
        private void SortByDoctorChecked(object sender, RoutedEventArgs e)
        {
            Anamnesis[] allAnamneses = GetData(patient);
            if (allAnamneses != null)
            {
                SortedDictionary<String, Anamnesis> dict = new SortedDictionary<String, Anamnesis>();
                foreach (Anamnesis anamnesis in allAnamneses)
                {
                    Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
                    Doctor doctor = Doctor.Find(Examination.FindExamination(anamnesis.ExaminationId, examinations).DoctorId);
                    String username = doctor.Username;
                    // sorting doctors by username
                    dict.Add(username, anamnesis);
                }
                GlobalAnamneses = dict.Values.ToArray();
                InitializeAnamnesesTable(GlobalAnamneses);
            }
            else
            {
                MessageBox.Show("Anamneses do not exist. \nSorry.");
            }
        }
        private void SortByDocSpecChecked(object sender, RoutedEventArgs e)
        {
            Anamnesis[] allAnamneses = GetData(patient);
            if (allAnamneses != null)
            {
                SortedDictionary<Doctor.DoctorsSpeciality, Anamnesis> dict = new SortedDictionary<Doctor.DoctorsSpeciality, Anamnesis>();
                foreach (Anamnesis anamnesis in allAnamneses)
                {
                    Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
                    Doctor doctor = Doctor.Find(Examination.FindExamination(anamnesis.ExaminationId, examinations).DoctorId);
                    Doctor.DoctorsSpeciality speciality = doctor.Speciality;
                    // sorting doctors by speciality
                    dict.Add(speciality, anamnesis);
                }
                GlobalAnamneses = dict.Values.ToArray();
                InitializeAnamnesesTable(GlobalAnamneses);
            }
            else
            {
                MessageBox.Show("Anamneses do not exist. \nSorry.");
            }

        }
        private void ShowAnamnesisInfoClick(object sender, RoutedEventArgs e)
        {
            if (SelectedAnamnesis != null)
            {
                Anamnesis anamnesis = SelectedAnamnesis;
                PatientAnamnesisWindow window = new PatientAnamnesisWindow(anamnesis);
                window.Top = 100;
                window.Left = 800;
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select an anamnesis to show!", "Warning");
            }
        }
        private void ShowExaminationInfoClick(object sender, RoutedEventArgs e)
        {
            if (SelectedAnamnesis != null)
            {
                Anamnesis anamnesis = SelectedAnamnesis;
                Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
                PatientExaminationWindow window = new PatientExaminationWindow(Examination.FindExamination(anamnesis.ExaminationId, examinations));
                window.Top = 100;
                window.Left = 800;
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select an anamnesis to show!", "Warning");
            }
        }
        private void FindDocsByNameBtn(object sender, RoutedEventArgs e)
        {
            string keyword = keywordBox.Text;
            Doctor[] doctors = ChoosingDoctorService.FindByName(keyword);
            if (doctors.Length != 0)
            {
                InitializeDoctorsTable(doctors);
            }
            else if (doctors.Length == 0 && keyword != "")
            {
                MessageBox.Show("Doctors with selected name do not exist.\nSorry!", "Warning");
                InitializeDoctorsTable(Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json"));
            }
            keywordBox.Text = "";
        }

        private void FindDocsBySurnameBtn(object sender, RoutedEventArgs e)
        {
            string keyword = keywordBox.Text;
            Doctor[] doctors = ChoosingDoctorService.FindBySurame(keyword);
            if (doctors.Length != 0)
            {
                InitializeDoctorsTable(doctors);
            }
            else if (doctors.Length == 0 && keyword != "")
            {
                MessageBox.Show("Doctors with selected surname do not exist.\nSorry!", "Warning");
                InitializeDoctorsTable(Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json"));
            }
            keywordBox.Text = "";
        }

        private void FindDocsBySpecBtn(object sender, RoutedEventArgs e)
        {
            string[] specs = ChoosingDoctorService.GetSpecializations(GeneralMedSpec, SurgerySpec, CardiologySpec);
            Doctor[] doctors = ChoosingDoctorService.FindBySpec(specs);
            if (doctors.Length != 0)
            {
                InitializeDoctorsTable(doctors);
            }
            else if (doctors.Length == 0 && specs.Length != 0)
            {
                MessageBox.Show("Doctors with selected specialization/s do not exist.\nSorry!", "Warning");
                InitializeDoctorsTable(Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json"));
            }
            specsBox.SelectedItem = "";
        }

        private void SortDocsByNameChecked(object sender, RoutedEventArgs e)
        {
            if (ShownDoctors != null)
            {
                InitializeDoctorsTable(ChoosingDoctorService.SortByName(ShownDoctors));
            }
        }

        private void SortDocsBySurnameChecked(object sender, RoutedEventArgs e)
        {
            if (ShownDoctors != null)
            {
                InitializeDoctorsTable(ChoosingDoctorService.SortBySurname(ShownDoctors));
            }
        }

        private void SortDocsBySpecChecked(object sender, RoutedEventArgs e)
        {
            if (ShownDoctors != null)
            {
                InitializeDoctorsTable(ChoosingDoctorService.SortBySpeciality(ShownDoctors));
            }
        }

        private void SortDocsByRateChecked(object sender, RoutedEventArgs e)
        {
            if (ShownDoctors != null)
            {
                InitializeDoctorsTable(ChoosingDoctorService.SortByRate(ShownDoctors));
            }
        }

        private void RateDoctorClick(object sender, RoutedEventArgs e)
        {
            if (SelectedDoctor != null)
            {
                ChoosingDoctorService.RateDoctor(SelectedDoctor, patient);
                InitializeDoctorsTable(Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json"));
            }
            else
            {
                MessageBox.Show("Select a doctor to rate!", "Warning");
            }
        }

        private void CreateExamWithDocClick(object sender, RoutedEventArgs e)
        {
            if (SelectedDoctor != null)
            {
                ChoosingDoctorService.CreateExamWithDoctor(SelectedDoctor, patient);
            }
            else
            {
                MessageBox.Show("Select a doctor to create examination!", "Warning");
            }
        }

        public void CheckForNotification()
        {
            while (true)
            {
                // checking for medicine notification
                MedicineReminder[] medicineReminders = NotificationService.GetPatientMedReminders(patient);
                if (medicineReminders.Length != 0)
                {       
                    foreach (MedicineReminder reminder in medicineReminders)
                    {
                        if (reminder.timer > 0)
                        {
                            Prescription prescription = reminder.prescription;
                            int dose = int.Parse(prescription.Schedule.Split("x")[0]);
                            bool medicineNeeded = NotificationService.IsTimeForMedicine(prescription, reminder.timer);
                            if (medicineNeeded)
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("You have to take " + dose.ToString() + " pill/s of " +
                                        prescription.Medicine.Name + " in the next " + reminder.timer.ToString() + " minute/s.\n"
                                        + "This medicine should be taken " + prescription.Meals + ".", "Reminder");
                                });
                            }
                        }
                    }
                }
                // checking for personal notification
                OtherReminder[] otherReminders = NotificationService.GetPatientOtherRemindersForDays(patient);
                if (otherReminders.Length != 0)
                {
                    foreach (OtherReminder reminder in otherReminders)
                    {
                        bool notificationNeeded = NotificationService.IsTimeForNotification(reminder, reminder.timer);
                        if (notificationNeeded)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show("Description: " + reminder.description + "\nIn the next "
                                    + reminder.timer.ToString() + " minute/s.", "Reminder name: " + reminder.name);
                            });
                        }
                    }
                }
                Thread.Sleep(50000);      //check every min
            }
        }

        private void SetReminderClick(object sender, RoutedEventArgs e)
        {
            PatientReminderManagementWindow patientMedReminderWindow = new PatientReminderManagementWindow(patient);
            patientMedReminderWindow.Show();
        }

        private void ReminderManagementClick(object sender, RoutedEventArgs e)
        {
            ManageOtherReminderWindow otherRemindersWindow = new ManageOtherReminderWindow(patient);
            otherRemindersWindow.Show();
        }

        private void OpenHospitalSurveyClick(object sender, RoutedEventArgs e)
        {
            HospitalSurveyView hospitalSurveyView = new HospitalSurveyView(patient);
            hospitalSurveyView.Show();
        }
    }
}
