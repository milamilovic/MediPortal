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
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.CommunicationSystem
{
    /// <summary>
    /// Interaction logic for PersonalChatsView.xaml
    /// </summary>
    public partial class PersonalChatsView : Window
    {
        public PersonalChatsView(bool isDoctor, string username)
        {
            InitializeComponent();
            DataContext = new PersonalChatsViewModel(isDoctor, username);

        }
    }
}
