using SpeedyPass.Controllers;
using System.Windows;

namespace SpeedyPass
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new MainWindowController();
        }
    }
}
