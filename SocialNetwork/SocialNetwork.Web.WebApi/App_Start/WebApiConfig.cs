using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SocialNetwork.Services;
using Ninject;
using SocialNetwork.Bot;
using SocialNetwork.Services.CSVExport;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.PhotoService;
using SocialNetwork.Services.Providers;

namespace SocialNetwork.Web.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var kernel = new StandardKernel();

            // services dependency injections
            kernel.Bind<AuthenticationService>().To<AuthenticationService>().InSingletonScope();
            kernel.Bind<IMessangerService>().To<MessangerService>().InSingletonScope();
            kernel.Bind<IUserSearchingService>().To<UserSearchingService>().InSingletonScope();
            kernel.Bind<MessageSearchingService>().To<MessageSearchingService>().InSingletonScope();
            kernel.Bind<RegistrationService>().To<RegistrationService>().InSingletonScope();
            kernel.Bind<CsvUserExportService>().To<CsvUserExportService>().InSingletonScope();
            kernel.Bind<CsvMessageExportService>().To<CsvMessageExportService>().InSingletonScope();
            kernel.Bind<IFriendsService>().To<FriendsService>().InSingletonScope();
            kernel.Bind<UserValidationService>().To<UserValidationService>().InSingletonScope();
            kernel.Bind<IMessageSearchingService>().To<MessageSearchingService>().InSingletonScope();
            kernel.Bind<IMessageHistoryService>().To<MessagesHistoryService>().InSingletonScope();
            kernel.Bind<IShortestUserPathService>().To<ShortestUserPathService>().InSingletonScope();
            kernel.Bind<IPathProvider>().To<PathProvider>().InSingletonScope();
            kernel.Bind<IImageService>().To<ImageService>().InSingletonScope();
            kernel.Bind<IEndPointSetter>().To<AuthenticationService>().InSingletonScope();


            config.DependencyResolver = new NinjectResolver(kernel);
            // Run Bot1
            var botRunner = new BotRunner();
            botRunner.Run();

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
