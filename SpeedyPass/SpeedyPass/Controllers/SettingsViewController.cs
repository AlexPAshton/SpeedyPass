using CustomUnity;
using SpeedyPass.Models;
using SpeedyPass.Services;
using SpeedyPass.ViewModels;
using SpeedyPass.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpeedyPass.Controllers
{
    public class SettingsViewController : ISettingsViewController
    {
        private ISettingsView view;
        private ISettingsViewModel viewModel;

        private IAppConfigService appConfigService;

        public SettingsViewController()
        {
            this.view = CustomUnityContainer.Resolve<ISettingsView>();
            this.viewModel = CustomUnityContainer.Resolve<ISettingsViewModel>();

            this.appConfigService = CustomUnityContainer.Resolve<IAppConfigService>();

            this.view.BindViewModel(ref this.viewModel);
            this.view.BindController(this);

            this.view.Show();
        }

        public void ContinueClicked()
        {
            ISettingsViewModel viewModel = this.viewModel;
            AppConfigModel oldAppConfigModel = this.appConfigService.AppConfigModel;

            this.appConfigService.AppConfigModel = new AppConfigModel
            {
                Setup = true,
                DataPath = oldAppConfigModel.DataPath,
                UseDirectInput = viewModel.UseDirectPasswordInput,
                UseClipboardInput = viewModel.UseClipboardPasswordInput,
                UsePin = viewModel.ProtectPasswordsWithPIN,
                Pin = null,
            };

            if (this.appConfigService.AppConfigModel.UsePin &&
                this.appConfigService.AppConfigModel.Pin == null)
            {
                CustomUnityContainer.Resolve<ISetPinViewController>();
            }
            else
            {
                CustomUnityContainer.Resolve<IDoneViewController>();
            }

            this.view.Close();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
