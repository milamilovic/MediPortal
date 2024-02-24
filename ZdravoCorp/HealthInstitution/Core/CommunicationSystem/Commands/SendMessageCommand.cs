using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Services;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Commands
{
    public class SendMessageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public ChatViewModel ChatViewModel;
        public PersonalChatsViewModel PersonalChatsViewModel;

        public SendMessageCommand(ChatViewModel chatViewModel, PersonalChatsViewModel personalChatsViewModel)
        {
            ChatViewModel = chatViewModel;
            PersonalChatsViewModel = personalChatsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ChatService chatService = new ChatService(new ChatRepository());
            chatService.SendMessage(ChatViewModel.NewMessage, ChatViewModel.Chat);
            ChatViewModel.NewMessage = "";
            ChatViewModel.InitializeChat();
            PersonalChatsViewModel.InitializeChats();
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
