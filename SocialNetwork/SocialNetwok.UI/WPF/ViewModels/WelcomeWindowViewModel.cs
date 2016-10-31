using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.UI.WPF.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.WPF.ViewModels
{
    public class WelcomeWindowViewModel : INotifyPropertyChanged
    {
        private readonly UserSearcher userSerchingService;

        private readonly FriendsService friendsService; 

        private readonly Authenticator authenticatorService;

        private readonly Services.UserValidationService userValidationService;

        private readonly bool canExecute = true;

        private ICommand logInCommand;

        private string userLogin;

        private string mailForRegistration;

        private string loginForRegistration;

        private string name;

        private string surname;

        private ICommand registrationCommand;
       
        private bool windowVisibility = true;

        public WelcomeWindowViewModel(
            Authenticator authenticatorServ, 
            FriendsService friendsServ,
            UserSearcher userSerchingServ,
            Services.UserValidationService valServ)
        {
            authenticatorService = authenticatorServ;
            friendsService = friendsServ;
            userValidationService = valServ;
            userSerchingService = userSerchingServ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserLogin
        {
            get
            {
                return userLogin;
            }

            set
            {
                userLogin = value;
                NotifyPropertyChanged("UserLogin");
            }
        }

        public string MailForRegistration
        {
            get
            {
                return mailForRegistration;
            }

            set
            {
                mailForRegistration = value;
                NotifyPropertyChanged("MailForRegistration");
            }
        }

        public string LoginForRegistration
        {
            get
            {
                return loginForRegistration;
            }

            set
            {
                loginForRegistration = value;
                NotifyPropertyChanged("LoginForRegistration");
            }
        }

        public string Password
        {
            get; set;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                surname = value;
                NotifyPropertyChanged("Surname");
            }
        }

        public SessionInfo CurrentSession { get; set; }

        /// <summary>
        ///     Set or get visibility for MainWindow
        /// </summary>
        public bool WindowVisibility
        {
            get
            {
                return windowVisibility;
            }

            set
            {
                windowVisibility = value;
                NotifyPropertyChanged("WindowVisibility");
            }
        }

        public ICommand LogInCommand
        {
            get { return logInCommand ?? (logInCommand = new CommandButtonHandler(param => Login(param), canExecute)); }
        }

        public ICommand RegistrationCommand
        {
            get
            {
                return registrationCommand ?? (registrationCommand =
                    new CommandButtonHandler(Registration, canExecute));
            }
        }

        public void NotifyPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Registration(object param)
        {
            Password = (param as PasswordBox)?.Password;
            string messageText;

            if (!userValidationService.ValidPassword(Password))
            {
                messageText = Resources.ToShortPassword;
            }
            else if (!userValidationService.ValidEmail(MailForRegistration))
            {
                messageText = Resources.IncorrectEmail;
            }
            else
            {
                RegistrationDTO regModel = new RegistrationDTO
                {
                    Email = MailForRegistration,
                    FirstName = Name,
                    LastName = Surname,
                    Password = Password,
                    ConfirmPassword = Password,
                    UserName = LoginForRegistration
                };

                if (authenticatorService.Register(regModel) != "BadRequest")
                { 
                    friendsService.AddFriends(
                        userSerchingService.GetByEmail(MailForRegistration).ResultTask.Result.Id,
                        userSerchingService.GetByEmail("bot@gmail.com").ResultTask.Result.Id);

                    CleanRegisterData(param);
                    messageText = Resources.SuccessfulRegistration;
                }
                else
                { 
                    messageText = Resources.UserWithMailOrLoginExist;
                }
            }

            MessageBox.Show(messageText);
        }

        private void Login(object param)
        {
            Password = (param as PasswordBox).Password;

            if (Password != string.Empty && UserLogin != string.Empty)
            {
                Response<SessionInfo> response = authenticatorService.LogIn(new LoginDTO(UserLogin, Password));
 
                if (response.IsSuccessful)
                {
                    CurrentSession = response.ResultTask.Result;
                    if (CurrentSession != null && CurrentSession.IsLogged)
                    {
                        WindowVisibility = false;

                        new UserProfile(CurrentSession).ShowDialog();
                        CleanLoginAndPassword(param);
                        WindowVisibility = true;
                    }
                    else
                    {
                        MessageBox.Show(Resources.incorrectLoginOrPassword);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.incorrectLoginOrPassword);
                }
            }
            else
            {
                MessageBox.Show(Resources.MustEnterLoginAndPassword);
            }
        }

        private void CleanLoginAndPassword(object param)
        {
            UserLogin = string.Empty;
            (param as PasswordBox).Password = string.Empty;
        }

        private void CleanRegisterData(object param)
        {
            UserLogin = string.Empty;
            MailForRegistration = string.Empty;
            Name = string.Empty;
            Surname = string.Empty;
            LoginForRegistration = string.Empty;
            (param as PasswordBox).Password = string.Empty;
        }
    }
}