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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using ZdravoCorp.HealthInstitution.GUI;
using System.Xaml;
using ZdravoCorp.HealthInstitution.Core.Referrals.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Services;
using ZdravoCorp.HealthInstitution.Core.Equipment.Services;
using ZdravoCorp.HealthInstitution.Core.Medicines.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Repository;

namespace ZdravoCorp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeEquipment();
            InitializeMedicine();
            InitializeRoomsAndTreatments();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password.ToString();
            LoginRepository repository = new LoginRepository();
            LoginService service = new LoginService(repository);
            bool loggedIn = service.LoginLogic(username, password);
            if (loggedIn)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Username and password are not valid, please try again!");
                usernameTextBox.Text = "";
                passwordBox.Password = "";
            }
        }

        private void InitializeEquipment()
        {
            EquipmentOrderingService.UpdateDeliveredOrders();
            EquipmentMovingService.UpdateMoveRequests(false);
        }
        private void InitializeMedicine()
        {
            MedicineRestockingService.RestockMedicine();
        }
        private void InitializeRoomsAndTreatments()
        {
            MedicalTreatmentReferralService.UpdateTreatments();
            PatientCareRoomsServices.UpdateRooms();
        }
    }
}