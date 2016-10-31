using Ninject;
using SocialNetwork.Services;
using SocialNetwork.Services.Authentication;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.PhotoService;
using SocialNetwork.Services.Providers;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.WPF
{
    internal static class NinjectKernel
    {
        static NinjectKernel()
        {
            Kernel = new StandardKernel();
            /*
            Kernel.Bind<AuthenticationService>().To<AuthenticationService>().InSingletonScope(); 
            Kernel.Bind<RegistrationService>().To<RegistrationService>().InSingletonScope();
            Kernel.Bind<MessageSearchingService>().To<MessageSearchingService>().InSingletonScope();
            Kernel.Bind<MessagesHistoryService>().To<MessagesHistoryService>().InSingletonScope();
            Kernel.Bind<UserSearchingService>().To<UserSearchingService>().InSingletonScope();
            Kernel.Bind<MessangerService>().To<MessangerService>().InSingletonScope();
            Kernel.Bind<UserValidationService>().To<UserValidationService>().InSingletonScope();
            */
            Kernel.Bind<UsersPhotoService>().To<UsersPhotoService>();
            Kernel.Bind<IPathProvider>().To<PathProvider>();
            Kernel.Bind<ImageService>().To<ImageService>().InSingletonScope();
            Kernel.Bind<Authenticator>().To<Authenticator>();
            Kernel.Bind<UserSearcher>().To<UserSearcher>();
            Kernel.Bind<Web.WebApiWrapper.Services.FriendsService>().To<Web.WebApiWrapper.Services.FriendsService>();
            Kernel.Bind<MessageHistoryService>().To<MessageHistoryService>();
            Kernel.Bind<Web.WebApiWrapper.Services.ShortestUserPathService>()
                .To<Web.WebApiWrapper.Services.ShortestUserPathService>();
            Kernel.Bind<MessageSearcher>().To<MessageSearcher>();
            Kernel.Bind<Messanger>().To<Messanger>().InSingletonScope();
        }

        public static IKernel Kernel { get; set; }
    }
}