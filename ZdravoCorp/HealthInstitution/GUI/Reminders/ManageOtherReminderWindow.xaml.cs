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
    /// Interaction logic for ManageOtherReminderWindow.xaml
    /// </summary>
    public partial class ManageOtherReminderWindow : Window
    {
        static Patient Patient;
        public OtherReminder SelectedReminder { get; set; }
        public ManageOtherReminderWindow(Patient patient)
        {
            Patient = patient;
            DataContext = this;
            InitializeComponent();
            InitializeReminderTable();
        }

        public void InitializeReminderTable()
        {
            dataGrid.ItemsSource = NotificationService.GetPatientOtherReminders(Patient);
        }

        private void CreateReminderClick(object sender, RoutedEventArgs e)
        {
            OtherReminderCRUD window = new OtherReminderCRUD(Patient, null);
            window.Show();
            this.Close();
        }

        private void UpdateReminderClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReminder != null)
            {
                OtherReminderCRUD window = new OtherReminderCRUD(Patient, SelectedReminder);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a reminder to update!", "Warning");
            }
        }

        private void DeleteReminderClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReminder != null)
            {
                NotificationService.DeleteOtherReminder(SelectedReminder);
                InitializeReminderTable();
                MessageBox.Show("Reminder successfully deleted!", "Confirmation");
            }
            else
            {
                MessageBox.Show("Select a reminder to delete!", "Warning");
            }
        }
    }
}
