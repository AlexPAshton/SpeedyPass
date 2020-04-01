using System;

namespace SpeedyPass.Views
{
    public interface ISetupViewModel
    {
        event ViewModels.SetupViewModel.VerifyArgs OnVerifyInput;

        string ConfigFileSavePath { get; set; }
        string ConfigFileSavePathValidity { get; set; }
        bool ContinueButtonEnabled { get; set; }
    }
}
