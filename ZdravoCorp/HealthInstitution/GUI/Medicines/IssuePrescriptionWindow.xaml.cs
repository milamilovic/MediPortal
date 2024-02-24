using System;
using System.Collections.Generic;
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
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Services;

namespace ZdravoCorp.HealthInstitution.GUI.DoctorWindows
{
    /// <summary>
    /// Interaction logic for IssuePrescriptionWindow.xaml
    /// </summary>
    public partial class IssuePrescriptionWindow : Window
    {
        public int PatientId;
        public string[] Allergies;
        public IssuePrescriptionWindow(int patientId, string[] allergies)
        {
            PatientId = patientId;
            Allergies = allergies;
            InitializeComponent();
            InitializeCmb();
        }

        private void InitializeCmb()
        {
            Medicine[] medicines = Medicine.LoadFile();
            foreach (Medicine medicine in medicines)
            {
                cmbMedicine.Items.Add(medicine.Name);
            }
        }

        private void BtnIssuePerscriptionClick(object sender, RoutedEventArgs e)
        {
            if (cmbMedicine.SelectedItem != null && txtDuration.Text != "" && txtSchedule.Text != "")
            {
                Medicine medicine = Medicine.FindMedicine(cmbMedicine.SelectedItem.ToString());
                if (!IssuePrescriptionService.CheckForAllergies(medicine, Allergies))
                {
                    string meals = "";
                    if (rbtBefore.IsChecked == true)
                    {
                        meals = "before meal";
                    }
                    else if (rbtAfter.IsChecked == true)
                    {
                        meals = "after meal";
                    }
                    try
                    {
                        int days = int.Parse(txtDuration.Text);

                        DateTime now = DateTime.Now;
                        string date = now.ToString("dd.MM.yyyy.");

                        Prescription prescription = new Prescription(PatientId, medicine, days, txtSchedule.Text, meals, date, false);
                        IssuePrescriptionService.IssuePrescription(prescription);
                        MessageBox.Show("Issued perscription");
                    }
                    catch
                    {
                        MessageBox.Show("Days of therapy must be a number");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Patient is allergic to this medicine or the ingrediants of the medicine");
                }
            }
            else
            {
                MessageBox.Show("Input empty", "Warning");
            }

        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
