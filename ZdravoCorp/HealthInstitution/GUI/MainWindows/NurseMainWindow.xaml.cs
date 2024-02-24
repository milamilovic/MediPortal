using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using ZdravoCorp.HealthInstitution.GUI.CRUD;
using ZdravoCorp.HealthInstitution.GUI.Register;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Examinations.Services;
using ZdravoCorp.HealthInstitution.Core.Anamneses.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Services;
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;
using ZdravoCorp.HealthInstitution.Core.Medicines.Services;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Services;
using ZdravoCorp.HealthInstitution.MedicalTreatment;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Repository;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.View;

namespace ZdravoCorp.HealthInstitution.GUI
{
    /// <summary>
    /// Interaction logic for NurseMainWindow.xaml
    /// </summary>
    public partial class NurseMainWindow : Window
    {
        public static ObservableCollection<Patient> Patients { get; set; }
        public static ObservableCollection<PatientOnTreatmentItem> PatientsOnTreatment { get; set; }
        public static ObservableCollection<Medicine> Medicines { get; set; }
        public Patient SelectedPatient { get; set; }
        public PatientOnTreatmentItem SelectedPatientOnTreatment { get; set; }
        public Medicine SelectedMedicine { get; set; }
        string Username;

        public Doctor.DoctorsSpeciality SelectedSpecialty { get; set; }

        public NurseMainWindow(Nurse loggedInNurse)
        {
            InitializeComponent();
            DataContext = this;
            Username = loggedInNurse.Username;
            InitializeProfile(loggedInNurse);
            InitializePatientTable();
            InitializeMedicineTable();
            InitializeExaminationsForToday();
            InitializePatientsOnTreatmentTable();
        }
        //General tab
        public void LogOutClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        public void InitializeProfile(Nurse loggedInNurse)
        {
            name.Content += "    " + loggedInNurse.Name;
            surname.Content += "    " + loggedInNurse.Surname;
            username.Content += "    " + loggedInNurse.Username;
            password.Content += "    " + loggedInNurse.Password;
            birthday.Content += "    " + loggedInNurse.Birthday;
            email.Content += "    " + loggedInNurse.Email;
            role.Content += "    " + "Nurse";
        }

        //Patients tab
        public void InitializePatientTable()
        {
            Patient[] patients = Patient.LoadPatients("../../../Data/PatientInfo/PatientData.json");
            Patients = new ObservableCollection<Patient>(Patient.LoadPatients("../../../Data/PatientInfo/PatientData.json"));
        }
        public static void UpdatePatientsCollection()
        {
            Patient[] patients = Patient.LoadPatients("../../../Data/PatientInfo/PatientData.json");
            Patients.Clear();
            foreach (var pat in patients)
            {
                Patients.Add(pat);
            }
        }
        public void RegisterPatient(object sender, RoutedEventArgs e)
        {
            RegisterPatientWindow register = new RegisterPatientWindow();
            register.Show();
        }

        public void DeletePatient(object sender, EventArgs e)
        {
            if (SelectedPatient != null)
            {
                PatientCRUD.DeletePatient(SelectedPatient);
                MessageBox.Show("Patient successfully deleted.");
                UpdatePatientsCollection();
            } 
            else
            {
                MessageBox.Show("Pick the patient you want to delete.");
            }
        }

        public void UpdatePatient(object sender, EventArgs e)
        {
            if (SelectedPatient != null)
            {
                UpdatePatientWindow update = new UpdatePatientWindow(SelectedPatient);
                update.Show();
            }
            else
            {
                MessageBox.Show("Pick the patient you want to update.");
            }
        }
        private void ScheduleExaminationThroughReferral(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                bool wasScheduled = ExaminationThroughReferralService.ScheduleExaminationThroughReferral(SelectedPatient);
                if(!wasScheduled)
                {
                    MessageBox.Show("Patient does not have an active referral.");
                } else
                {
                    MessageBox.Show("Examination was scheduled successfully.");
                }
            }
            else
            {
                MessageBox.Show("Pick a patient to find a referral.");
            }

        }

        private void StartMedicalTreatment(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                MedicalTreatmentReferral referral = MedicalTreatmentReferralService.FindPatientsReferral(SelectedPatient);
                if (referral != null)
                {
                    MedicalTreatmentView medicalTreatmentView = new MedicalTreatmentView(SelectedPatient, referral);
                    medicalTreatmentView.Show();
                }
                else
                {
                    MessageBox.Show("Patient does not have an active referral.");
                }
            }
            else
            {
                MessageBox.Show("Select a patient first.");
            }
        }

        private void BuyMedicine(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                Prescription prescription = MedicinePurchasement.FindPrescription(SelectedPatient);
                if (prescription == null) { 
                    MessageBox.Show("Patient does not have an active prescription.");
                }
                else if (!MedicinePurchasement.IsLastDosageTaken(prescription))
                {
                    MessageBox.Show("Last treatment is not over, patient still has enough medicine.");
                }
                else if (!MedicinePurchasement.IsMedicineStocked(prescription.Medicine))
                {
                    MessageBox.Show("Wanted medicine is out of supply.");
                } else
                {
                    MedicinePurchasement.PurchaseMedicine(SelectedPatient, prescription);
                    MessageBox.Show("Medicine '" +prescription.Medicine.Name+ "' is available, total price is: " + prescription.Medicine.Price.ToString());
                }
            }
            else
            {
                MessageBox.Show("Pick a patient to find a prescription.");
            }
        }

        //Examinations tab
        public void InitializeExaminationsForToday()
        {
            listExaminations.ItemsSource = Examination.GetActiveExaminationsForToday();
        }

        public void AdmitPatient(object sender, EventArgs e)
        {
            if (listExaminations.SelectedItem != null)
            {
                Examination selectedExamination = ((Examination)listExaminations.SelectedItem);
                if (CanAdmitionBeStarted(selectedExamination))
                {
                    CreateAnamnesisWindow anamnesisWindow = new CreateAnamnesisWindow(selectedExamination.Id, selectedExamination.PatientId);
                    anamnesisWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Select an examination");
            }
        }

        private bool CanAdmitionBeStarted(Examination examination)
        {
            DateTime now = DateTime.Now;
            now = now.AddMinutes(15);
            DateTime startTime = TimeSlot.GetStartTime(examination.TimeSlot);
            if (now.CompareTo(startTime) <= 0)
            {
                MessageBox.Show("Admission can be started only 15 minutes before examination");
                return false;
            }
            if (Anamnesis.DoesAnamnesisExist(examination.Id))
            {
                MessageBox.Show("Anamnesis was already created");
                return false;
            }
            return true;
        }

        //Emergency Examination tab
        public void ScheduleEmergencyExamination(object sender, EventArgs e)
        {
            if (SelectedPatient != null)
            {
                if(WasSpecialitySelected(sender, e))
                {
                    bool wasScheduled = EmergencyExaminationService.TryToSchedule(SelectedPatient, SelectedSpecialty);
                    if (wasScheduled)
                    {
                        MessageBox.Show("Emergency examination was successfuly scheduled.");
                    } else
                    {
                        Dictionary<Examination, TimeSpan> fiveExaminations = EmergencyExaminationService.GetFiveLeastImportantExaminations(SelectedSpecialty);
                        CreateEmergencyExaminationWindow examWindow = new CreateEmergencyExaminationWindow(fiveExaminations, SelectedPatient);
                        examWindow.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Pick the patient you want to update.");
            }
        }

        public bool WasSpecialitySelected(object sender, EventArgs e)
        {
            if (surgeryOption.IsChecked == true)
            {
                SelectedSpecialty = Doctor.DoctorsSpeciality.Surgery;
            }
            else if (generalMedicineOption.IsChecked == true)
            {
                SelectedSpecialty = Doctor.DoctorsSpeciality.GeneralMedicine;
            }
            else if (cardiologyOption.IsChecked == true)
            {
                SelectedSpecialty = Doctor.DoctorsSpeciality.Cardiology;
            }
            else {
                MessageBox.Show("Pick a speciality for the examination.");
                return false;
            }
            return true;
        }

        //Medicine tab
        public void InitializeMedicineTable()
        {
            Medicines = new ObservableCollection<Medicine>(Medicine.LoadFile());
        }

        //Medical treatment tab
        public void InitializePatientsOnTreatmentTable()
        {
            PatientsOnTreatment = new ObservableCollection<PatientOnTreatmentItem>(PatientOnTreatmentRepository.LoadPatientsCurrentlyOnTreatment());
        }

        private void MakeDailyReport(object sender, RoutedEventArgs e)
        {
            if (SelectedPatientOnTreatment != null)
            {
                NurseVisitView nurseVisitView = new NurseVisitView(SelectedPatientOnTreatment);
                nurseVisitView.Show();
            } 
            else
            {
                MessageBox.Show("Pick a patient.");
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnChatsWindowClick(object sender, RoutedEventArgs e)
        {
            PersonalChatsView chatsWindow = new PersonalChatsView(false, Username);
            chatsWindow.Show();
        }


    }
}
