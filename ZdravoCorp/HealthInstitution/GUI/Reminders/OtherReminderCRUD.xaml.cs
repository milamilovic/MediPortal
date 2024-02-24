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
    /// Interaction logic for OtherReminderCRUD.xaml
    /// </summary>
    public partial class OtherReminderCRUD : Window
    {
        Patient Patient;
        OtherReminder oldReminder;
        public OtherReminderCRUD(Patient patient, OtherReminder reminder)
        {
            Patient = patient;
            InitializeComponent();
            InitializeData(reminder);
        }

        public void InitializeData(OtherReminder reminder)
        {
            // create reminder
            if (reminder == null)
            {
                updateButton.IsEnabled = false;
                createButton.IsEnabled = true;
            }
            // update reminder
            else
            {
                oldReminder = reminder;
                updateButton.IsEnabled = true;
                createButton.IsEnabled = false;
                nameBox.Text = reminder.Name;
                descriptionBox.Text = reminder.description;
                daysBox.Text = reminder.Days.ToString();
                pickDate.SelectedDate = Convert.ToDateTime(reminder.StartDate);
                timesBox.Text = reminder.times.ToString();
                timerBox.Text = reminder.timer.ToString();
            }
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "" || descriptionBox.Text == "" || pickDate.SelectedDate == null
                || timerBox.Text == "" || timesBox.Text == "" || daysBox.Text == "")
            {
                MessageBox.Show("Not everything is filled out!");
            }
            else
            {
                try
                {
                    bool create = true;
                    string startDate = pickDate.SelectedDate.Value.Date.ToString("dd.MM.yyyy.");
                    int timer = int.Parse(timerBox.Text);
                    if (timer <= 0)
                    {
                        create = false;
                        MessageBox.Show("Timer must be greater than zero!", "Error");
                    }
                    int times = int.Parse(timesBox.Text);
                    if (times <= 0)
                    {
                        create = false;
                        MessageBox.Show("Times per day must be greater than zero!", "Error");
                    }
                    int days = int.Parse(daysBox.Text);
                    if (days <= 0)
                    {
                        create = false;
                        MessageBox.Show("Days must be greater than zero!", "Error");
                    }
                    if (create)
                    {
                        NotificationService.CreateOtherReminder(nameBox.Text, descriptionBox.Text,
                            startDate, days, times, Patient.Id, timer);
                        this.Close();
                    }
                }
                catch { MessageBox.Show("Invalid input types", "Warning"); }
            }
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "" || descriptionBox.Text == "" || pickDate.SelectedDate == null
                || timerBox.Text == "" || timesBox.Text == "" || daysBox.Text == "")
            {
                MessageBox.Show("Not everything is filled out!");
            }
            else
            {
                try
                {
                    bool update = true;
                    string startDate = pickDate.SelectedDate.Value.Date.ToString("dd.MM.yyyy.");
                    int timer = int.Parse(timerBox.Text);
                    if (timer <= 0)
                    {
                        update = false;
                        MessageBox.Show("Timer must be greater than zero!", "Error");
                    }
                    int times = int.Parse(timesBox.Text);
                    if (times <= 0)
                    {
                        update = false;
                        MessageBox.Show("Times per day must be greater than zero!", "Error");
                    }
                    int days = int.Parse(daysBox.Text);
                    if (days <= 0)
                    {
                        update = false;
                        MessageBox.Show("Days must be greater than zero!", "Error");
                    }
                    if (update)
                    {
                        NotificationService.UpdateOtherReminder(oldReminder, nameBox.Text, descriptionBox.Text,
                            startDate, days, times, Patient.Id, timer);
                        this.Close();
                    }
                }
                catch { MessageBox.Show("Invalid input types", "Warning"); }
            }
        }
    }
}
