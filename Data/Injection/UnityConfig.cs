using System;
using System.Reflection;
using System.Web.Configuration;
using Data.DbContext;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Data.Injection
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();
            container.RegisterType<DbEntities>(new PerHttpRequestLifetimeManager<WebContext>());

            Assembly assemblyDataService = Assembly.Load("Service");
            Assembly assemblyDataServiceInterface = Assembly.Load("Service.Interface");

            foreach (var interfaceType in assemblyDataServiceInterface.GetTypes())
            {
                var serviceTypeName = "Service." + interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);
                var serviceType = assemblyDataService.GetType(serviceTypeName);
                if (serviceType != null)
                {
                    container.RegisterType(interfaceType, serviceType, new PerHttpRequestLifetimeManager(serviceType))
                    .Configure<Interception>().SetInterceptorFor(interfaceType, new InterfaceInterceptor());
                }
            }

            Assembly assemblyDataRepository = Assembly.Load("Repository");
            Assembly assemblyDataRepositoryInterface = Assembly.Load("Repository.Interface");

            foreach (var interfaceType in assemblyDataRepositoryInterface.GetTypes())
            {
                var repositoryTypeName = "Repository." + interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);
                var repositoryType = assemblyDataRepository.GetType(repositoryTypeName);
                var key = repositoryType.Name.Replace("Repository", string.Empty);
                if (!HttpContainer.RepositoryTypes.ContainsKey(key))
                {
                    HttpContainer.RepositoryTypes.Add(key, repositoryType);
                }
                container.RegisterType(interfaceType, repositoryType, new PerHttpRequestLifetimeManager(repositoryType))
                       .Configure<Interception>().SetInterceptorFor(interfaceType, new InterfaceInterceptor());
            }
        }
    }
}
