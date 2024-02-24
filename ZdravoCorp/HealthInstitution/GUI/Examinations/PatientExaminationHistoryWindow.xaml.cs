using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Anamneses.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Services;

namespace ZdravoCorp.HealthInstitution.GUI.CRUD
{
    public partial class PatientExaminationHistoryWindow : Window
    {
        public Patient Patient;
        public static ObservableCollection<Examination> allExaminations { get; set; }
        public Examination SelectedExaminationHistory { get; set; }
        public PatientExaminationHistoryWindow(Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            DataContext = this;
            InitializeExaminationHistoryTable(patient);
        }

        public static void InitializeExaminationHistoryTable(Patient patient)
        {
            Examination[] examinations = Examination.LoadExaminations("../../../Data/Examinations/Examinations.json");
            List<Examination> patientExaminationsList = new List<Examination>();
            foreach (Examination ex in examinations)
            {
                DateTime examinationDateTime = DateTime.ParseExact(ex.TimeSlot.Date + " " + ex.TimeSlot.StartTime,
                "dd.MM.yyyy. HH:mm:ss", null);
                // showing only past examinations
                if (examinationDateTime.CompareTo(DateTime.Now) < 0)
                {
                    if (ex.PatientId == patient.Id)
                    {
                        patientExaminationsList.Add(ex);
                    }
                }
            }
            Examination[] patientExaminations = patientExaminationsList.ToArray();
            allExaminations = new ObservableCollection<Examination>(patientExaminations);
        }

        private void ShowAnamnesisClick(object sender, RoutedEventArgs e)
        {
            if (SelectedExaminationHistory != null)
            {
                Anamnesis anamnesis = Anamnesis.Find(SelectedExaminationHistory.Id);
                if (anamnesis == null)
                {
                    MessageBox.Show("Anamnesis does not exist. \nSorry.", "Error");
                }
                else
                {
                    PatientAnamnesisWindow window = new PatientAnamnesisWindow(anamnesis);
                    window.Top = 100;
                    window.Left = 800;
                    window.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Select an examination to show anamnesis!", "Warning");
            }
        }

        private void RateDoctorClick(object sender, RoutedEventArgs e)
        {
            if (SelectedExaminationHistory != null && SelectedExaminationHistory.State != "deleted")
            {
                Doctor doctor = Doctor.Find(SelectedExaminationHistory.DoctorId);
                ChoosingDoctorService.RateDoctor(doctor, Patient);
            }
            else
            {
                MessageBox.Show("Select a past examination to rate doctor!", "Warning");
            }
        }

        private void FindAnamnesisClick(object sender, RoutedEventArgs e)
        {
            PatientFindAnamnesesWindow window = new PatientFindAnamnesesWindow(Patient);
            window.Top = 100;
            window.Left = 500;
            window.ShowDialog();
        }
    }
}
