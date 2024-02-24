using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.CommunicationSystem;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Services
{
    public class ChatService
    {
        private readonly IChatRepository _repository; 
        public ChatService(IChatRepository repository) 
        { 
            _repository = repository;
        }

        public void Add(Message message, Chat chat)
        {
            Chat[] chats = _repository.Load();
            Chat ch = Find(chats, chat);
            ch.Messages.Add(message);
            ch.LastMessage = message;
            _repository.Save(chats);
        }

        public Chat Find(Chat[] chats, Chat chat)
        {
            foreach (Chat ch in chats)
            {
                if ((ch.Sender.Username.Equals(chat.Sender.Username) && ch.Recipient.Username.Equals(chat.Recipient.Username))
                    || (ch.Sender.Username.Equals(chat.Recipient.Username) && ch.Recipient.Username.Equals(chat.Sender.Username)))
                {
                    return ch;
                }
            }
            return null;
        }

        public void SendMessage(string messageContent, Chat chat)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss");
            Message message = new Message(messageContent, chat.Sender, dateTime);
            Add(message, chat);
        }
    }
}
