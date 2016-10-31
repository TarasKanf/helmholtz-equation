using System.Windows;
using Ninject;
using SocialNetwork.UI.WPF.ViewModels;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.WPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var auth = NinjectKernel.Kernel.Get<Authenticator>();
            var friendSession = NinjectKernel.Kernel.Get<FriendsService>();
            var userSerchingServ = NinjectKernel.Kernel.Get<UserSearcher>();
            var userValidationService = NinjectKernel.Kernel.Get<Services.UserValidationService>();
            
            DataContext = new WelcomeWindowViewModel(auth, friendSession, userSerchingServ, userValidationService);
        }
    }
}