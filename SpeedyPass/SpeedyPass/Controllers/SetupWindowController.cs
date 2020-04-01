using CustomUnity;
using SpeedyPass.Views;
using System.IO;
using System.Windows;

namespace SpeedyPass.Controllers
{
    public class SetupWindowController : ISetupWindowController
    {
        private ISetupView view;
        private ISetupViewModel viewModel;

        public SetupWindowController()
        {
            this.view = CustomUnityContainer.Resolve<ISetupView>();
            this.viewModel = CustomUnityContainer.Resolve<ISetupViewModel>();

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
            new MainWindowController(this.viewModel.ConfigFileSavePath);
            this.view.Close();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
