using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;

namespace SpeedyPass.Views
{
    public interface ISetPinView
    {
        void BindViewModel(ref ISetPinViewModel viewModel);
        void BindController(ISetPinViewController setPinViewController);
        void Show();
        void Close();
    }
}