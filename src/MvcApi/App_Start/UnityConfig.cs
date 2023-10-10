using MvcApi.Business.Assets;
using MvcApi.Business.Interfaces;
using Unity;

namespace MvcApi
{
    /// <summary>
    ///
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAssetService, AssetService>();

            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}