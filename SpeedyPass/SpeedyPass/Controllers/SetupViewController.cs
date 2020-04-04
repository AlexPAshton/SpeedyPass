using CustomUnity;
using SpeedyPass.Services;
using SpeedyPass.Views;
using System.IO;
using System.Windows;

namespace SpeedyPass.Controllers
{
    public class SetupViewController : ISetupViewController
    {
        private const string PASSWORDDATA_PATH = "SpeedyPassDat.dat";

        private ISetupView view;
        private ISetupViewModel viewModel;

        private IAppConfigService appConfigService;

        public SetupViewController()
        {
            this.view = CustomUnityContainer.Resolve<ISetupView>();
            this.viewModel = CustomUnityContainer.Resolve<ISetupViewModel>();

            this.appConfigService = CustomUnityContainer.Resolve<IAppConfigService>();

            this.view.BindViewModel(ref this.viewModel);
            this.view.BindController(this);

            this.VerifyInput();

            this.view.Show();
        }

        public void VerifyInput()
        {
            bool inputValid = Directory.Exists(this.viewModel.ConfigFileSavePath);

            this.viewModel.ConfigFileSavePathValidity = inputValid ? string.Empty : "Invalid";

            this.viewModel.ContinueButtonEnabled = inputValid;
        }

        public void ContinueClicked()
        {
            this.appConfigService.AppConfigModel.DataPath = string.Format("{0}{1}", 
                this.viewModel.ConfigFileSavePath,
                SetupViewController.PASSWORDDATA_PATH);

            CustomUnityContainer.Resolve<ISettingsViewController>();

            this.view.Close();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
