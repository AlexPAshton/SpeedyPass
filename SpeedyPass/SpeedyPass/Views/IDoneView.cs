using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;

namespace SpeedyPass.Views
{
    public interface IDoneView
    {
        void BindViewModel(ref IDoneViewModel viewModel);
        void BindController(IDoneViewController setPinViewController);
        void Show();
        void Close();
    }
}