using SQLite;
using System.IO;
using TForTea.Managers;
using TForTea.WinRT.Models;
using TForTea.WinRT.ViewModel;
using TForTea.WinRT.Views;
using ViewManagement;
using Windows.UI.Xaml.Controls;

namespace TForTea.WinRT
{
    public class BootStrap
    {
        private static IoCManager _ioc;

        internal static void Init(ContentControl windowContent)
        {
            InitIoCManager();
            InitLogManager();
            InitDatabase();
            InitViewManager(windowContent);

            var viewManager = _ioc.Resolve<ViewManager>();
            viewManager.Init(windowContent);
            viewManager.OpenView<MainViewModel>();
        }

        private static void InitDatabase()
        {
            var dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (var db = new SQLiteConnection(dbPath))
            {
                db.CreateTable<Tea>();
            }

            var dbManager = new DatabaseManager(dbPath, _ioc.Resolve<LogManager>());
            _ioc.RegisterInstance(dbManager);
        }

        private static void InitIoCManager()
        {
            _ioc = new IoCManager();
            _ioc.RegisterInstance(_ioc);
        }

        private static void InitLogManager()
        {
            _ioc.RegisterType<LogManager>(true);
        }

        private static void InitViewManager(ContentControl windowContent)
        {
            _ioc.RegisterType<ViewManager>(true);

            var viewManager = _ioc.Resolve<ViewManager>();
            viewManager.Init(windowContent);
            RegisterViews(viewManager);
        }

        private static void RegisterViews(ViewManager viewManager)
        {
            viewManager.RegisterViewModel<MainViewModel, MainPage>();
        }
    }
}