using System.Windows;

namespace SpeedyPass.Views
{
    public interface ISetupView
    {
        WindowState WindowState { get; set; }

        void Show();
        void BindController(ISetupViewController controller);
        void BindViewModel(ref ISetupViewModel viewModel);
        void DragMove();
        void Close();
    }
}
