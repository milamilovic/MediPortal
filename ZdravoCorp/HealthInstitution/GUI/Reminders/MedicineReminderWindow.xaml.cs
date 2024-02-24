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
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Model;
using ZdravoCorp.HealthInstitution.Core.Reminders.Services;

namespace ZdravoCorp.HealthInstitution.GUI.PatientWindows
{
    public partial class MedicineReminderWindow : Window
    {
        public Prescription Prescription;
        public MedicineReminderWindow(MedicineReminder reminder)
        {
            Prescription = reminder.prescription;
            InitializeComponent();
            InitializeData(Prescription);
        }

        public void InitializeData(Prescription prescription)
        {
            medTitle.Content += ": " + prescription.Medicine.Name;
            dateBox.Text = prescription.Date;
            dateBox.IsEnabled = false;
            daysBox.Text = prescription.Days.ToString();
            daysBox.IsEnabled = false;
            scheduleBox.Text = prescription.Schedule;
            scheduleBox.IsEnabled = false;
            mealsBox.Text = prescription.Meals;
            mealsBox.IsEnabled = false;
        }

        public int GetData()
        {
            try
            {
                int timerMins = int.Parse(timeBox.Text);
                if (timerMins <= 0)
                {
                    MessageBox.Show("Timer must be greater than 0!", "Error");
                }
                else
                {
                    return timerMins;
                }
            }
            catch { MessageBox.Show("Invalid input", "Error"); }
            return 0;
        }

        private void SaveReminderClick(object sender, RoutedEventArgs e)
        {
            int timer = GetData();
            NotificationService.SaveMedicineReminderTimer(Prescription, timer);
            this.Close();
        }
    }
}
