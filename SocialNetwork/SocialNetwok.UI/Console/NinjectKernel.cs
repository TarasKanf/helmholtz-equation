using Ninject;
using SocialNetwork.Services.CSVExport;
using SocialNetwork.UI.Console.InputOutput;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console
{
    /// <summary>
    ///     Configures all solution dependencies.
    /// </summary>
    public static class NinjectKernel
    {
        private static readonly IKernel Kernel;

        static NinjectKernel()
        {
            Kernel = new StandardKernel();

            // console dependency injections
            Kernel.Bind<IInputOutputSevice>().To<ConsoleService>().InSingletonScope();
            Kernel.Bind<CommandParser>().To<CommandParser>().InSingletonScope();

            // services dependency injections
            Kernel.Bind<CsvUserExportService>().To<CsvUserExportService>().InSingletonScope();
            Kernel.Bind<CsvMessageExportService>().To<CsvMessageExportService>().InSingletonScope();

            Kernel.Bind<Messanger>().To<Messanger>().InSingletonScope();
            Kernel.Bind<Authenticator>().To<Authenticator>();
            Kernel.Bind<UserSearcher>().To<UserSearcher>();
            Kernel.Bind<FriendsService>().To<FriendsService>();
            Kernel.Bind<MessageHistoryService>().To<MessageHistoryService>();
            Kernel.Bind<ImageUrlService>().To<ImageUrlService>();
            Kernel.Bind<SocialNetwork.Web.WebApiWrapper.Services.ShortestUserPathService>()
                .To<SocialNetwork.Web.WebApiWrapper.Services.ShortestUserPathService>();
        }

        /// <summary>
        ///     Gets an instance of specified service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.TryGet<T>();
        }
    }
}   