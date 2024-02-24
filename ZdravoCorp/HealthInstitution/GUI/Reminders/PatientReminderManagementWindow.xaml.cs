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
using ZdravoCorp.HealthInstitution.Core.Reminders.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.GUI.PatientWindows
{
    /// <summary>
    /// Interaction logic for PatientReminderManagementWindow.xaml
    /// </summary>
    public partial class PatientReminderManagementWindow : Window
    {
        static Patient Patient;
        public MedicineReminder SelectedReminder { get; set; }
        public PatientReminderManagementWindow(Patient patient)
        {
            Patient = patient;
            DataContext = this;
            InitializeComponent();
            InitializePrescriptionTable();
        }

        public void InitializePrescriptionTable()
        {
            dataGrid.ItemsSource = NotificationService.GetPatientMedReminders(Patient);
        }

        private void SetTimerClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReminder != null)
            {
                MedicineReminderWindow medicineReminder = new MedicineReminderWindow(SelectedReminder);
                medicineReminder.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a prescription to set timer!", "Warning");
            }
        }

        private void DeleteTimerClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReminder != null)
            {
                NotificationService.DeleteMedicineReminder(SelectedReminder);
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a prescription to delete reminder!", "Warning");
            }
        }
    }
}
