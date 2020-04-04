namespace SpeedyPass.ViewModels
{
    public interface ISetPinViewModel
    {
        event ViewModels.SetPinViewModel.VerifyArgs OnVerifyInput;

        string InputChar0 { get; set; }
        string InputChar1 { get; set; }
        string InputChar2 { get; set; }
        string InputChar3 { get; set; }
        string InputChar0Validity { get; set; }
        string InputChar1Validity { get; set; }
        string InputChar2Validity { get; set; }
        string InputChar3Validity { get; set; }
        bool ContinueButtonEnabled { get; set; }
    }
}
