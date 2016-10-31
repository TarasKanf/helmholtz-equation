using System.Windows;
using Ninject;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services.PhotoService;
using SocialNetwork.UI.WPF.ViewModels;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.WPF
{
    /// <summary>
    ///     Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        public UserProfile(SessionInfo session)
        {
            InitializeComponent();

            var userSearchingService = NinjectKernel.Kernel.Get<UserSearcher>();
            var messageHistoryService = NinjectKernel.Kernel.Get<MessageHistoryService>();
            var friendService = NinjectKernel.Kernel.Get<FriendsService>();
            var messengerService = NinjectKernel.Kernel.Get<Messanger>();
            var messageSerchingServ = NinjectKernel.Kernel.Get<MessageSearcher>();
            var profilePhotoService = NinjectKernel.Kernel.Get<Services.PhotoService.UsersPhotoService>();
            var imageServ = NinjectKernel.Kernel.Get<ImageService>();
            var auth = NinjectKernel.Kernel.Get<Authenticator>();
            var imageUrlService = NinjectKernel.Kernel.Get<ImageUrlService>();

            DataContext = new UserProfileViewModel(
                messageHistoryService, 
                userSearchingService,
                friendService,
                messengerService,
                session,
                messageSerchingServ,
                profilePhotoService,
                imageServ,
                auth,
                imageUrlService);
        }
    }
}