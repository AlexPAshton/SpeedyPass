namespace SpeedyPass.ViewModels
{
    public interface ISettingsViewModel
    {
        bool UseDirectPasswordInput { get; set; }
        bool UseClipboardPasswordInput { get; set; }
        bool ProtectPasswordsWithPIN { get; set; }
    }
}
