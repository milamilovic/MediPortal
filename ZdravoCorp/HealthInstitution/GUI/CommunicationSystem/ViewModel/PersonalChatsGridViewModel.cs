using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel
{
    public class PersonalChatsGridViewModel : INotifyPropertyChanged
    {
        public Profile Sender { get; set; }
        public Profile Recipient { get; set; }
        public List <Message> Messages { get; set; }
        public Message LastMessage { get; set; }
        internal PersonalChatsGridViewModel(Chat chat, bool isDoctor)
        {
            if (isDoctor) 
            { 
                Sender = chat.Sender; 
                Recipient = chat.Recipient;
            }
            else 
            { 
                Sender = chat.Recipient; 
                Recipient = chat.Sender;
            }
            Messages = chat.Messages;
            LastMessage = chat.LastMessage;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
