using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using ZdravoCorp.HealthInstitution.GUI.DoctorWindows;
using ZdravoCorp.HealthInstitution.Core.Examinations.Model;
using ZdravoCorp.HealthInstitution.Core.Examinations.Services;
using ZdravoCorp.HealthInstitution.Core.Anamneses.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;

namespace ZdravoCorp.HealthInstitution.GUI.CRUD
{
    /// <summary>
    /// Interaction logic for PerformExamWindow.xaml
    /// </summary>
    public partial class PerformExamWindow : Window
    {
        public Examination Exam;
        public int RoomId;
        public IssueReferralService IssueReferralService;
        public PerformExamWindow(Examination exam, int roomId)
        {
            Exam = exam;
            RoomId = roomId;
            ExaminationReferralRepository examinationRepository = new ExaminationReferralRepository();
            MedicalTreatmentReferralRepository treatmentRepository = new MedicalTreatmentReferralRepository();
            IssueReferralService =new IssueReferralService(treatmentRepository,examinationRepository);
            InitializeComponent();
            InitializePatientGrid();
            InitializeDoctorGrid();
            InitializeAnamnesisData();
        }


        private void InitializeAnamnesisData()
        {
            if (Anamnesis.DoesAnamnesisExist(Exam.Id))   //if the anamnesis exist,load data
            {
                Anamnesis anamnesis = Anamnesis.FindAnamnesis(Exam.Id);
                txtSymptoms.Text = anamnesis.Symptoms;
                txtAllergies.Text = anamnesis.Alergies;
                txtIllness.Text = anamnesis.EarlierIllnesses;
                txtDiagnosis.Text = anamnesis.Diagnosis;
            }

        }

        private void InitializePatientGrid()
        {
            List<Patient> patients = new List<Patient>();
            patients.Add(Patient.Find(Exam.PatientId));
            patientDataGrid.ItemsSource = patients;
        }
        private void InitializeDoctorGrid()
        {

            Doctor[] doctors = Doctor.LoadDoctors("../../../Data/DoctorInfo/DoctorData.json");
            doctorDataGrid.ItemsSource = doctors;
            List<Doctor.DoctorsSpeciality> specialities = Enum.GetValues(typeof(Doctor.DoctorsSpeciality)).Cast<Doctor.DoctorsSpeciality>().ToList();
            cmbSpeciality.ItemsSource = specialities;
        }

        private void BtnViewRecordClick(object sender, RoutedEventArgs e)
        {
            Patient patient = Patient.Find(Exam.PatientId);
            UpdateRecordWindow recordWindow = new UpdateRecordWindow(patient.GetMedicalRecord(), Exam.DoctorId, Exam.PatientId, true);
            recordWindow.ShowDialog();
            InitializePatientGrid();

        }

        private void BtnUpdateAnamnesisClick(object sender, RoutedEventArgs e)
        {

            if (txtSymptoms.Text != "" && txtIllness.Text != "" && txtAllergies.Text != "" &&
                txtDiagnosis.Text != "")
            {
                Anamnesis anamnesis = new Anamnesis(Exam.Id, Exam.PatientId, txtSymptoms.Text, txtAllergies.Text, txtIllness.Text);
                anamnesis.Diagnosis = txtDiagnosis.Text;
                if (Anamnesis.DoesAnamnesisExist(Exam.Id))
                {
                    PerformExaminationService.UpdateAnamnesis(anamnesis);
                }
                else
                {
                    PerformExaminationService.CreateAnamnesis(anamnesis);
                }
                MessageBoxResult result = MessageBox.Show("Issue a perscription", "Perscription", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string[] allergies = txtAllergies.Text.Split(",");
                    IssuePrescriptionWindow prescriptionWindow = new IssuePrescriptionWindow(Exam.PatientId, allergies);
                    prescriptionWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Not all data is entered", "Warning");
            }
        }

        private void BtnEndExamClick(object sender, RoutedEventArgs e)
        {
            DynamicEquipmentWindow equipmentWindow = new DynamicEquipmentWindow(RoomId);
            equipmentWindow.Show();
            this.Close();
        }

        private void BtnIssueDoctorClick(object sender, RoutedEventArgs e)
        {
            if (doctorDataGrid.SelectedItem != null && txtDescrtiption.Text != "")
            {
                Doctor doctor = doctorDataGrid.SelectedItem as Doctor;
                IssueReferralService.IssueExaminationReferral(doctor, Exam.PatientId, null, txtDescrtiption.Text);
                MessageBox.Show("Issued referral");

            }
            else
            {
                MessageBox.Show("Fill out required fields", "Warning");
            }
        }

        private void BtnIssueSpecialityClick(object sender, RoutedEventArgs e)
        {
            if (cmbSpeciality.SelectedItem != null && txtDescrtiption.Text != "")
            {
                Doctor.DoctorsSpeciality speciality = (Doctor.DoctorsSpeciality)cmbSpeciality.SelectedItem;
                List<Doctor> specilisedDoctors = Doctor.FindSpecialisedDoctors(speciality);
                if (specilisedDoctors.Count != 0)
                {
                    IssueReferralService.IssueExaminationReferral(specilisedDoctors[0], Exam.PatientId, null, txtDescrtiption.Text);
                    MessageBox.Show("Issued referral");
                }
                else
                {
                    MessageBox.Show("No doctors with this speciality");
                }
            }
            else
            {
                MessageBox.Show("Fill out required fields", "Warning");
            }
        }

        private void BtnHospitalTreatmentClick(object sender, RoutedEventArgs e)
        {

            if (txtDays.Text != "" && txtTherapy.Text != ""
                 && dtpFrom.SelectedDate != null && dtpTo.SelectedDate != null)
            {
                int numDays = int.Parse(txtDays.Text);
                if (IssueReferralService.CheckDates(dtpFrom.SelectedDate.Value, dtpTo.SelectedDate.Value, numDays))
                {
                    try
                    {
                        IssueReferralService.IssueMedicalTreatment(Exam.DoctorId, Exam.PatientId, numDays
                            , txtTherapy.Text, txtExaminations.Text,
                            dtpFrom.SelectedDate.Value.ToString("dd.MM.yyyy."), dtpTo.SelectedDate.Value.ToString("dd.MM.yyyy."));
                        MessageBox.Show("Issued referral");

                    }
                    catch
                    {
                        MessageBox.Show("Days must be a number", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong dates");
                }

            }
            else
            {
                MessageBox.Show("Enter days and therapy for the treatment", "Warning");
            }
        }
    }
}
