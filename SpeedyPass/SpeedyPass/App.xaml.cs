using CustomUnity;
using SpeedyPass.Controllers;
using SpeedyPass.Services;
using SpeedyPass.Views;
using System.Windows;

namespace SpeedyPass
{
    public partial class App : Application
    {
        private const string APPNAME = "SpeedPass";
        public const string EXCEPTION_COULDNOTRESOLVE = "IServiceStorageAppConfig could not be resolved.";

        private IServiceStorageAppConfig serviceStorageAppConfig;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.serviceStorageAppConfig = CustomUnityContainer.Resolve<IServiceStorageAppConfig>();

            if (this.serviceStorageAppConfig != null)
            {
                if (this.serviceStorageAppConfig.ConfigExists())
                {
                    new MainWindowController(); // OLD CODE
                }
                else
                {
                    CustomUnityContainer.Resolve<ISetupWindowController>();
                }
            }
            else
            {
                ExceptionMessageBox.Show(App.APPNAME, App.EXCEPTION_COULDNOTRESOLVE);
            }
        }
    }
}
