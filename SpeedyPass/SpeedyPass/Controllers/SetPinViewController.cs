using CustomUnity;
using SpeedyPass.Services;
using SpeedyPass.ViewModels;
using SpeedyPass.Views;
using System.Windows;

namespace SpeedyPass.Controllers
{
    public class SetPinViewController : ISetPinViewController
    {
        private const string PASSWORDDATA_PATH = "SpeedyPassDat.dat";

        private ISetPinView view;
        private ISetPinViewModel viewModel;

        private IAppConfigService appConfigService;
        private IDataEncryptionService dataEncryptionService;

        public SetPinViewController()
        {
            this.view = CustomUnityContainer.Resolve<ISetPinView>();
            this.viewModel = CustomUnityContainer.Resolve<ISetPinViewModel>();

            this.appConfigService = CustomUnityContainer.Resolve<IAppConfigService>();
            this.dataEncryptionService = CustomUnityContainer.Resolve<IDataEncryptionService>();

            this.viewModel.OnVerifyInput += this.OnVerifyInput;

            this.view.BindViewModel(ref this.viewModel);
            this.view.BindController(this);

            this.view.Show();
        }

        private void OnVerifyInput(int index)
        {
           if (index == 0)
            {
                if (string.IsNullOrEmpty(this.viewModel.InputChar0) || 
                    !int.TryParse(this.viewModel.InputChar0, out int n))
                {
                    this.viewModel.InputChar0Validity = "Invalid";
                }
                else
                {
                    this.viewModel.InputChar0Validity = string.Empty;
                }
            }
            else if (index == 1)
            {
                if (string.IsNullOrEmpty(this.viewModel.InputChar1) || 
                    !int.TryParse(this.viewModel.InputChar1, out int n))
                {
                    this.viewModel.InputChar1Validity = "Invalid";
                }
                else
                {
                    this.viewModel.InputChar1Validity = string.Empty;
                }
            }
            else if (index == 2)
            {
                if (string.IsNullOrEmpty(this.viewModel.InputChar2) || 
                    !int.TryParse(this.viewModel.InputChar2, out int n))
                {
                    this.viewModel.InputChar2Validity = "Invalid";
                }
                else
                {
                    this.viewModel.InputChar2Validity = string.Empty;
                }
            }
            else if (index == 3)
            {
                if (string.IsNullOrEmpty(this.viewModel.InputChar3) || 
                    !int.TryParse(this.viewModel.InputChar3, out int n))
                {
                    this.viewModel.InputChar3Validity = "Invalid";
                }
                else
                {
                    this.viewModel.InputChar3Validity = string.Empty;
                }
            }

           if (this.viewModel.InputChar0Validity == string.Empty &&
                this.viewModel.InputChar1Validity == string.Empty &&
                this.viewModel.InputChar2Validity == string.Empty &&
                this.viewModel.InputChar3Validity == string.Empty)
            {
                this.viewModel.ContinueButtonEnabled = true;
            }
           else
            {
                this.viewModel.ContinueButtonEnabled = false;
            }
        }

        public void ContinueClicked()
        {
            string fullEnteredPin = string.Format("{0}{1}{2}{3}", this.viewModel.InputChar0, this.viewModel.InputChar1, this.viewModel.InputChar2, this.viewModel.InputChar3);

            this.dataEncryptionService.Salt = fullEnteredPin;
           
            string encryptedEnteredPin = this.dataEncryptionService.Encrypt(fullEnteredPin);

            this.appConfigService.AppConfigModel.Pin = encryptedEnteredPin;

            CustomUnityContainer.Resolve<IDoneViewController>();

            this.view.Close();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
