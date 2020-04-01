using SpeedyPass.Views;
using System;

namespace SpeedyPass.ViewModels
{
    public class SetupViewModel : ViewModel, ISetupViewModel
    {
        public delegate void VerifyArgs();
        public event VerifyArgs OnVerifyInput;

        public string ConfigFileSavePath 
        { 
            get { return this.configFileSavePath; } 
            set { base.SetField(ref this.configFileSavePath, value); this.VerifyInput(); }
        }
        public string ConfigFileSavePathValidity 
        { 
            get { return this.configFileSavePathValidity; }
            set { base.SetField(ref this.configFileSavePathValidity, value); }
        }
        public bool ContinueButtonEnabled
        {
            get { return this.continueButtonEnabled; }
            set { base.SetField(ref this.continueButtonEnabled, value); }
        }

        private string configFileSavePathValidity = "";
        private string configFileSavePath = "C:/";
        private bool continueButtonEnabled = false;

        public void VerifyInput() => this.OnVerifyInput?.Invoke();
    }
}
