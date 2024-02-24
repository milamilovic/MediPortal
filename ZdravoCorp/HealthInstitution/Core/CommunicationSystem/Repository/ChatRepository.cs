using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;

namespace ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository
{
    public class ChatRepository : IChatRepository
    {
        public Chat[] LoadPersonal(string username)
        {
            Chat[] chats = Load();
            return chats.Where(chat => chat.Sender.Username.Equals(username) ||
                chat.Recipient.Username.Equals(username)).ToArray();
        }


        public Chat[] Load()
        {
            var jsontext = File.ReadAllText("../../../Data/CommunicationSystem/Chats.json");
            Chat[] allChats = JsonConvert.DeserializeObject<Chat[]>(jsontext)!;
            return allChats;
        }

        public void Save(Chat[] chats)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string chatsJson = System.Text.Json.JsonSerializer.Serialize(chats, options);
            File.WriteAllText("../../../Data/CommunicationSystem/Chats.json", chatsJson);
        }
    }
}
