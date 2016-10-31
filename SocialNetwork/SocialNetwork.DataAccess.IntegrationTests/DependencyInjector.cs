using System;
using System.IO;
using System.Reflection;
using Ninject;
using SocialNetwork.DataAccess.Context;
using SocialNetwork.DataAccess.Repositories;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services.UnitTest
{
    public class DependencyInjector
    {
        private readonly IKernel kernel = new StandardKernel();

        public DependencyInjector()
        {
            AddBiddings();
        }

        public string PathFolder { get; set; }

        public T Get<T>()
        {
            return kernel.Get<T>();
        }

        private void AddBiddings()
        {
            string applicationDirectory =
                new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            PathFolder = applicationDirectory + @"/Resources/";
            kernel.Bind<IRepository<Connection>>()
                .To<XmlRepository<Connection>>()
                .WithConstructorArgument("c", new XmlContext(PathFolder));
            kernel.Bind<IRepository<User>>()
                .To<XmlRepository<User>>()
                .WithConstructorArgument("c", new XmlContext(PathFolder));
            kernel.Bind<IRepository<Message>>()
                .To<XmlRepository<Message>>()
                .WithConstructorArgument("c", new XmlContext(PathFolder));
        }
    }
}