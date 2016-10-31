using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.PhotoService;
using SocialNetwork.UI.WPF.Properties;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.WPF.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly bool canExecute;
        private readonly ServerListener listener = new ServerListener();
        private readonly MessageHistoryService messageHistoryService;
        private readonly Messanger messengerService;
        private readonly UserSearcher userSearchingService;
        private readonly Services.PhotoService.UsersPhotoService profilePhotoService;
        private readonly Authenticator authenticatorService;
        private readonly ImageUrlService imageUrlService;
        private bool controlsVisibility;
        private SessionInfo currentSession;
        private double docWidth;
        private ICommand findFriendCommand;
        private ICommand changePrifilePhoto;
        private ICommand windowClosing;
        private string findFriendEmailOrLogin;
        private int fontSize;
        private List<UserModel> friends;
        private FriendsService friendService;
        private string imageSource;
        private UserModel interlocutor;
        private string message;
        private FlowDocument messages;
        private MessageSearcher messageSearchingService;
        private UserModel selectedFriend;
        private ICommand sendMessageCommand;
        private ImageService imageService;

        public UserProfileViewModel(
            MessageHistoryService messageHistoryServ,
            UserSearcher userSearchingServ,
            FriendsService friendServ,
            Messanger mesServ,
            SessionInfo session,
            MessageSearcher mesSerServ,
            Services.PhotoService.UsersPhotoService profilePhotoService,
            ImageService imageServ,
            Authenticator authenticatorServ,
            ImageUrlService imageUrlServ)
        {
            Messages = new FlowDocument();
            controlsVisibility = false;
            canExecute = true;
            CurrentSession = session;
            messageHistoryService = messageHistoryServ;
            userSearchingService = userSearchingServ;
            friendService = friendServ;
            this.profilePhotoService = profilePhotoService;
            imageService = imageServ;
            Friends = friendService.GetFriends(currentSession.LoggedUser.Id).ResultTask.Result;
            authenticatorService = authenticatorServ;
            imageUrlService = imageUrlServ;

        FindFriendEmailOrLogin = string.Empty;
            messengerService = mesServ;
            DocWidth = 248;
            FontSize = 13;
            messageSearchingService = mesSerServ;
            var imageUrl = profilePhotoService.GetProfilePhoto(currentSession.LoggedUser.ProfilePhotoId).Url;
            if (imageUrl.Contains(@"\Resources\images\ProfilePhotos"))
            {
                ImageSource = imageUrl;
            }
            else
            {
                ImageSource = imageUrlService.GetImageUrl(currentSession.LoggedUser.ProfilePhotoId);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SessionInfo CurrentSession
        {
            get
            {
                return currentSession;
            }

            set
            {
                currentSession = value;
                currentSession.NewMessageRecieved += ShowNotifingAboutNewMessage;
                listener.Session = CurrentSession;

                try
                {
                    Task.Run(() =>
                    {
                        var response = listener.Connect(currentSession.SessionKey).Result;
                        if (response.IsSuccessful)
                        {
                            listener.StartListening();
                        }
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                NotifyPropertyChanged("CurrentSession");
            }
        }

        public string NameAndSurname
        {
            get
            {
                return string.Format("{0} {1}", currentSession.LoggedUser.FirstName, currentSession.LoggedUser.LastName);
            }
        }

        public string ImageSource
        {
            get
            {
                return imageSource;
            }

            set
            {
                imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }

        public double DocWidth
        {
            get
            {
                return docWidth;
            }

            set
            {
                docWidth = value;
                NotifyPropertyChanged("DocWidth");
            }
        }

        public int FontSize
        {
            get
            {
                return fontSize;
            }

            set
            {
                fontSize = value;
                NotifyPropertyChanged("FontSize");
            }
        }

        public List<UserModel> Friends
        {
            get
            {
                return friends;
            }

            set
            {
                friends = value;
                NotifyPropertyChanged("Friends");
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public UserModel SelectedFriend
        {
            get
            {
                return selectedFriend;
            }

            set
            {
                selectedFriend = value;
                selectedFriend.ProfilePhoto = profilePhotoService.GetProfilePhoto(selectedFriend.ProfilePhotoId);
                LoadMessagesHisstory();
                NotifyPropertyChanged("Messages");
                Message = string.Empty;
                ControlsVisibility = true;
                NotifyPropertyChanged("SelectedFriend");
            }
        }

        public FlowDocument Messages
        {
            get
            {
                return messages;
            }

            set
            {
                messages = value;
                NotifyPropertyChanged("Messages");
            }
        }

        /// <summary>
        ///     Set or get visibility for MessageGrid: When you choose friend only then you
        ///     can write message and watch message history
        /// </summary>
        public bool ControlsVisibility
        {
            get
            {
                return controlsVisibility;
            }

            set
            {
                controlsVisibility = value;
                NotifyPropertyChanged("ControlsVisibility");
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return sendMessageCommand ??
                       (sendMessageCommand = new CommandButtonHandler(param => SendMessage(), canExecute));
            }
        }

        public ICommand ChangeProfilePhoto
        {
            get
            {
                return changePrifilePhoto ??
                       (changePrifilePhoto = new CommandButtonHandler(param => ChangePhoto(), canExecute));
            }
        }

        public ICommand FindFriendCommand
        {
            get
            {
                return findFriendCommand ??
                       (findFriendCommand = new CommandButtonHandler(param => FindFriend(), canExecute));
            }
        }

        public ICommand WindowClosing
        {
            get
            {
                return windowClosing ??
                       (windowClosing = new CommandButtonHandler(param => OnWindowClose(), canExecute));
            }
        }

        public string FindFriendEmailOrLogin
        {
            get
            {
                return findFriendEmailOrLogin;
            }

            set
            {
                findFriendEmailOrLogin = value;
                NotifyPropertyChanged("FindFriendEmailOrLogin");
            }
        }

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void SendMessage()
        {
            AddMessageWithDesignToHistory(new Message
            {
                ReceiverId = interlocutor.Id,
                SenderId = currentSession.LoggedUser.Id,
                Date = DateTime.Now,
                Data = Message
            });

            if (Message != string.Empty)
            {
                await messengerService.SendAsync(currentSession.LoggedUser.Id, interlocutor.Id, Message);
            }

            Message = string.Empty;
            FindFriendEmailOrLogin = string.Empty;

            if (!IsFriend())
            {
                AddIntoFriendList();
            }
        }

        private void ChangePhoto() // changed form 'private'
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    WebClient client = new WebClient();
                    byte[] result = client.UploadFile(SocialNetwork.Web.WebApiWrapper.Services.BaseService.Host + Resources.ImageCreatePath, fileName);
                    var strID = System.Text.Encoding.Default.GetString(result);
                    var res = strID.Substring(1, strID.Length - 2);
                    var id = new Guid(res);

                    var image = imageService.Create(id, currentSession.LoggedUser.Id);
                    currentSession.LoggedUser.ProfilePhotoId = image.Id;
                    var imageUrl = profilePhotoService.GetProfilePhoto(currentSession.LoggedUser.ProfilePhotoId).Url;
                    if (imageUrl.Contains(@"\Resources\images\ProfilePhotos"))
                    {
                        ImageSource = imageUrl;
                    }
                    else
                    {
                        // ImageSource = profilePhotoService.GetProfilePhoto(currentSession.LoggedUser.ProfilePhotoId).Url;
                        ImageSource = imageUrlService.GetImageUrl(currentSession.LoggedUser.ProfilePhotoId);
                    }
                }
            }
        }

        private void ShowNotifingAboutNewMessage(object sender, SessionInfo.RecievedMessageEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => AddMessageWithDesignToHistory(e.Message)));
        }

        private void LoadMessagesHisstory()
        {
            Messages = new FlowDocument();
            interlocutor = userSearchingService.GetById(selectedFriend.Id).ResultTask.Result;

            var messages = messageHistoryService.GetAllMessageHistory(currentSession.LoggedUser.Id, interlocutor.Id).ResultTask.Result;

            foreach (var m in messages)
            {
                AddMessageWithDesignToHistory(m.ConverToMessage());
            }
        }

        private void OnWindowClose()
        {
            authenticatorService.LogOut(CurrentSession.SessionKey);
        }

        private void AddMessageWithDesignToHistory(Message m)
        {
            var p = new Paragraph();
            p.Inlines.Add(DesignMessage(m));
            p.Inlines.Add(DesignDate(m.Date));
            Messages.Blocks.Add(p);
        }

        private TextBlock DesignMessage(Message m)
        {
            var marginLeftForDate = 90;
            var marginBetweenResieverSender = 55;

            var mes = new TextBlock();
            mes.Text = m.Data;

            mes.Margin = m.SenderId == currentSession.LoggedUser.Id
                ? new Thickness(0, 0, marginBetweenResieverSender, 0)
                : new Thickness(marginBetweenResieverSender, 0, 0, 0);

            string color = m.SenderId == currentSession.LoggedUser.Id ? "#FFECB3" : "#FFF8E1";
            mes.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

            mes.MaxWidth = DocWidth - marginLeftForDate;
            mes.TextWrapping = TextWrapping.Wrap;

            return mes;
        }

        private TextBlock DesignDate(DateTime date)
        {
            var marginLeftForTime = 60;
            int coefficientOfTopDateMarging = -3;
            var marginLeftForDate = 90;

            var dateDesign = new TextBlock();

            dateDesign.Foreground = Brushes.LightGray;
            dateDesign.Background = Brushes.White;

            dateDesign.Margin = date.Date == DateTime.Now.Date
                ? new Thickness(DocWidth - marginLeftForTime, -FontSize + coefficientOfTopDateMarging, 0, 0)
                : new Thickness(DocWidth - marginLeftForDate, -FontSize + coefficientOfTopDateMarging, 0, 0);

            dateDesign.Text = date.Date == DateTime.Now.Date ? date.ToShortTimeString() : date.ToShortDateString();

            return dateDesign;
        }

        private void FindFriend()
        {
            var potentialFriendSearch = userSearchingService.GetByLoginOrEmail(FindFriendEmailOrLogin);
            if (potentialFriendSearch.IsSuccessful)
            {
                var potentialFriend = userSearchingService.GetByLoginOrEmail(FindFriendEmailOrLogin).ResultTask.Result;

                if (potentialFriend.Login != null)
                {
                    interlocutor = potentialFriend;
                    if (IsFriend())
                    {
                        SelectedFriend = interlocutor;
                    }
                    else
                    {
                        Messages = new FlowDocument();
                        Message = string.Empty;
                        ControlsVisibility = true;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.UserWithMailOrLoginDoesntExist);
                }
            }
            else
            {
                MessageBox.Show(Resources.UserWithMailOrLoginDoesntExist);
            }
        }

        private bool IsFriend()
        {
            return Friends.FirstOrDefault(c => c.Id == interlocutor.Id) != null;
        }

        private void AddIntoFriendList()
        {
            Friends.Add(interlocutor);
            NotifyPropertyChanged("Friends");
            SelectedFriend = interlocutor;            
        }
    }
}