using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpeedyPass.Views
{
    public interface ISettingsView
    {
        WindowState WindowState { get; set; }

        void Show();
        void BindController(ISettingsViewController controller);
        void BindViewModel(ref ISettingsViewModel viewModel);
        void DragMove();
        void Close();
    }
}
