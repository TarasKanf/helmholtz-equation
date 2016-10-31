using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;
using Ninject.Activation;

namespace SocialNetwork.Web.WebApi
{
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }

    public class NinjectScope : IDependencyScope
    {
        protected IResolutionRoot ResolutionRoot;
        public NinjectScope(IResolutionRoot kernel)
        {
            ResolutionRoot = kernel;
        }
        public object GetService(Type serviceType)
         {
            IRequest request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).SingleOrDefault();
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            IRequest request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).ToList();
        }
        public void Dispose()
        {
            IDisposable disposable = (IDisposable)ResolutionRoot;
            if (disposable != null) disposable.Dispose();
            ResolutionRoot = null;
        }
    }
}