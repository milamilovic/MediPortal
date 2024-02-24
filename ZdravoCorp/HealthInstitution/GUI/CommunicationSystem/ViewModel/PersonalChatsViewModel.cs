using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Commands;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel
{
    public class PersonalChatsViewModel : INotifyPropertyChanged
    {
        public bool IsDoctor { get; set; }
        public string Username { get; set; }
        private readonly ObservableCollection<PersonalChatsGridViewModel> _chats;
        public IEnumerable<PersonalChatsGridViewModel> Chats => _chats;
        private PersonalChatsGridViewModel _selectedChat { get; set; }
        public PersonalChatsGridViewModel SelectedChat { get => _selectedChat; set => _selectedChat = value; }

        public OpenChatCommand OpenChatCommand { get; }

        private int _selected;
        public int Selected
        {
            get => _selected;
            set
            {
                if (value < 0) { return; }
                _selected = value;
                OnPropertyChanged(nameof(Selected));
                _selectedChat = _chats.ElementAt(_selected);
            }
        }

        public PersonalChatsViewModel(bool isDoctor, string username)
        {
            IsDoctor = isDoctor;
            Username = username;
            _chats = new ObservableCollection<PersonalChatsGridViewModel>();
            OpenChatCommand = new OpenChatCommand(this);
            InitializeChats();
        }

        public void InitializeChats()
        {
            _chats.Clear();
            ChatRepository chatRepository = new ChatRepository();
            List<Chat> chats = chatRepository.LoadPersonal(Username).ToList();
            foreach (Chat chat in chats)
            {
                _chats.Add(new PersonalChatsGridViewModel(chat, IsDoctor));
            }
        }

        public Chat GetChat()
        {
            return new Chat(SelectedChat.Sender, SelectedChat.Recipient,
                SelectedChat.Messages, SelectedChat.LastMessage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
