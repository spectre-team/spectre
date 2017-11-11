[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Spectre.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Spectre.App_Start.NinjectWebCommon), "Stop")]

namespace Spectre.App_Start
{
    using System;
    using System.Configuration;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Service;
    using Service.Abstract;
    using Service.Configuration;
    using Service.Loaders;
    using Service.Scheduling;

    /// <summary>
    /// Binds Ninject IoC to application lifecycle.
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            _bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            _bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Rebind<DataRootConfig>().ToConstant(new DataRootConfig(
                    ConfigurationManager.AppSettings["LocalDataDirectory"],
                    ConfigurationManager.AppSettings["RemoteDataDirectory"]));

            kernel.Rebind<DatasetLoader>().ToSelf();
            kernel.Rebind<IDivikService>().To<DivikService>();
            kernel.Rebind<IJobScheduler>().To<JobScheduler>();
        }
    }
}
