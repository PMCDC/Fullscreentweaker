using FT.Core.Services;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace FT.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new Views.Main();
        }

        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(IProcessInteractorService), typeof(ProcessInteractorService));
            containerRegistry.Register(typeof(ICacheService), typeof(CacheService));
        }
    }
}
