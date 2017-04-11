namespace TForTea
{
    using Microsoft.Practices.Unity;
    using Prism.Unity.Windows;
    using Prism.Windows.AppModel;
    using System.Threading.Tasks;
    using TForTea.Managers;
    using TForTea.Services;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;

    public sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            this.Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            this.Container.RegisterType<LogManager>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<DatabaseManager>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<TeaProxyService>();

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate(nameof(PageTokens.Main), null);
            return Task.FromResult(true);
        }
    }
}