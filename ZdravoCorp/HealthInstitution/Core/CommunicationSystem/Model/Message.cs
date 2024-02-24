using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model
{
    public class Message
    {
        public string Content { get; set; }
        public Profile Sender { get; set; }
        public string DateAndTime { get; set; }

        public Message(string content, Profile sender, string dateAndTime)
        {
            Content = content;
            Sender = sender;
            DateAndTime = dateAndTime;
        }
    }
}
