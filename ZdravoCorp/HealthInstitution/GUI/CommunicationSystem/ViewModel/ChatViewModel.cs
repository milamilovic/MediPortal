using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Commands;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Model;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Repository;
using ZdravoCorp.HealthInstitution.Core.CommunicationSystem.Services;

namespace ZdravoCorp.HealthInstitution.GUI.CommunicationSystem.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public Chat Chat;
        public StackPanel ChatPanel;
        public Window PersonalChat;
        private string _newMessage;
        public SendMessageCommand SendMessageCommand { get; }

        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                if (_newMessage != value)
                {
                    _newMessage = value;
                    OnPropertyChanged(nameof(NewMessage)) ;
                }
            }
        }

        public ChatViewModel(Chat chat, StackPanel stackPanel, Window window, PersonalChatsViewModel allChatsViewModel)
        {
            Chat = chat;
            NewMessage = "";
            ChatPanel = stackPanel;
            PersonalChat = window;
            PersonalChat.Title = Chat.Recipient.Username;
            SendMessageCommand = new SendMessageCommand(this, allChatsViewModel);
            InitializeChat();
        }

        public void InitializeChat()
        {
            ChatPanel.Children.Clear();
            ChatPanel.Orientation = Orientation.Horizontal;
            ScrollViewer scrollViewer = MakeScrollViewer();
            StackPanel innerSp = MakeStackPanel();
            InitializeOldMessages(innerSp);
            scrollViewer.Content = innerSp;
            ChatPanel.Children.Add(scrollViewer);
        }

        public void InitializeOldMessages(StackPanel stackPanel)
        {
            ChatRepository chatRepository = new ChatRepository();
            ChatService chatService = new ChatService(chatRepository);
            Chat chat = chatService.Find(chatRepository.Load(), Chat);
            foreach (Message message in chat.Messages)
            {
                WrapPanel wp = MakeWrapPanel();
                wp.Children.Add(MakeLabel(message.Sender.Username + " @ "));
                wp.Children.Add(MakeLabel(message.DateAndTime));
                stackPanel.Children.Add(wp);
                stackPanel.Children.Add(MakeTextBlock(message.Content));
            }
        }

        public Label MakeLabel(string content)
        {
            Label label = new Label();
            label.Content = content;
            label.FontSize = 8;
            return label;
        }

        public WrapPanel MakeWrapPanel()
        {
            WrapPanel wp = new WrapPanel();
            wp.HorizontalAlignment = HorizontalAlignment.Left;
            return wp;
        }

        public TextBlock MakeTextBlock(string content)
        {
            TextBlock tb = new TextBlock();
            tb.Text = content;
            return tb;
        }

        public ScrollViewer MakeScrollViewer()
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.ScrollToEnd();
            scrollViewer.HorizontalAlignment = HorizontalAlignment.Right;
            return scrollViewer;
        }

        public StackPanel MakeStackPanel()
        {
            StackPanel innerSp = new StackPanel();
            innerSp.Margin = new Thickness(10, 0, 0, 0);
            innerSp.Width = 300;
            innerSp.VerticalAlignment = VerticalAlignment.Bottom;
            return innerSp;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
