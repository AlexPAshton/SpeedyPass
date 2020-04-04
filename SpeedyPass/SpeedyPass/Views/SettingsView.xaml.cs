using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpeedyPass.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window, ISettingsView
    {
        private ISettingsViewModel viewModel;
        private ISettingsViewController controller;

        public SettingsView()
        {
            this.InitializeComponent();
        }

        public void BindController(ISettingsViewController controller) => this.controller = controller;

        public void BindViewModel(ref ISettingsViewModel viewModel)
        {
            this.viewModel = viewModel;

            base.DataContext = this.viewModel;
        }

        public void DragMoveWindow()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        public void Minimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseIcon_MouseDown(object sender, MouseButtonEventArgs e) => this.controller.CloseApplication();

        private void MinimizeText_MouseDown(object sender, MouseButtonEventArgs e) => this.Minimize();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => this.DragMoveWindow();

        private void NextButtonBackground_MouseDown(object sender, MouseButtonEventArgs e) => this.controller.ContinueClicked();

        private void UseDirectPasswordInput_Checked(object sender, MouseButtonEventArgs e)
        {
            this.viewModel.UseDirectPasswordInput = !this.viewModel.UseDirectPasswordInput;
        }

        private void UseClipboardPasswordInput_Checked(object sender, MouseButtonEventArgs e)
        {
            this.viewModel.UseClipboardPasswordInput = !this.viewModel.UseClipboardPasswordInput;
        }

        private void ProtectPasswordsWithPIN_Checked(object sender, MouseButtonEventArgs e)
        {
            this.viewModel.ProtectPasswordsWithPIN = !this.viewModel.ProtectPasswordsWithPIN;
        }
    }
}
