using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Commands
{
    public class OpenChatCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private PersonalChatsViewModel personalChatsViewModel;

        public OpenChatCommand(PersonalChatsViewModel personalChatsViewModel)
        {
            this.personalChatsViewModel = personalChatsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (personalChatsViewModel.SelectedChat != null)
            {
                Chat chat = personalChatsViewModel.GetChat();
                ChatView chatView = new ChatView(chat, personalChatsViewModel);
                chatView.Show();
            }
            else { MessageBox.Show("Select a chat to open!", "Warning"); }
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
