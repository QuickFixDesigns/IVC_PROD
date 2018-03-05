using NeoTracker.Assets;
using NeoTracker.DAL;
using NeoTracker.Migrations;
using NeoTracker.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace NeoTracker
{
    /// <summary>
    /// Interaction logic forApp.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainViewModel vm = new MainViewModel();
        public static Navigation nav = new Navigation();
        public static string BlankStr = "_";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Database.SetInitializer(new Initializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<NeoTrackerContext, Configuration>());
        }
    }
}
