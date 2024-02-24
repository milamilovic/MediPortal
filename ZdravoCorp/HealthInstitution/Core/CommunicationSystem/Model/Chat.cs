using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model
{
    public class Chat
    {
        public Profile Sender { get; set; }
        public Profile Recipient { get; set; }
        public List<Message> Messages { get; set; }
        public Message LastMessage { get; set; }

        public Chat(Profile sender, Profile recipient, List<Message> messages, Message lastMessage)
        {
            Sender = sender;
            Recipient = recipient;
            Messages = messages;
            LastMessage = lastMessage;
        }
    }
}
