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
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.CommunicationSystem
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public ChatView(Chat chat, PersonalChatsViewModel oldViewModel)
        {
            InitializeComponent();
            DataContext = new ChatViewModel(chat, chatPanel, personalChat, oldViewModel);
        }
    }
}
