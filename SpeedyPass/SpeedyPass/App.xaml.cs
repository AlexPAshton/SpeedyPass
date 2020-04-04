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

        private enum StartupStates
        {
            NoException,
            UnityContainerException,
            AppConfigMissing,
            AppConfigNotSetup,
            AppConfigPinRequiredNotSet,
        }

        private IAppConfigService appConfigService;
        private StartupStates startupState;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.appConfigService = CustomUnityContainer.Resolve<IAppConfigService>();

            this.GetStartupState();

            this.ActOnStartupState();
        }

        private void GetStartupState()
        {
            if (this.appConfigService == null)
            {
                startupState = StartupStates.UnityContainerException;
            }
            else if (!this.appConfigService.IsCreated())
            {
                startupState = StartupStates.AppConfigMissing;
            }
            else if (startupState == StartupStates.NoException && !this.appConfigService.IsSetup())
            {
                startupState = StartupStates.AppConfigNotSetup;
            }
            else if (startupState == StartupStates.NoException && !this.appConfigService.IsPinSetIfRequired())
            {
                startupState = StartupStates.AppConfigPinRequiredNotSet;
            }
        }

        private void ActOnStartupState()
        {
            switch (this.startupState)
            {
                case StartupStates.NoException:
                    new MainWindowController(); // OLD CODE
                    break;
                case StartupStates.UnityContainerException:
                    ExceptionMessageBox.Show(App.APPNAME, App.EXCEPTION_COULDNOTRESOLVE);
                    break;
                case StartupStates.AppConfigMissing:
                    CustomUnityContainer.Resolve<ISetupViewController>();
                    break;
                case StartupStates.AppConfigNotSetup:
                    CustomUnityContainer.Resolve<ISettingsViewController>();
                    break;
                case StartupStates.AppConfigPinRequiredNotSet:
                    CustomUnityContainer.Resolve<ISetPinViewController>();
                    break;
            }
        }
    }
}
