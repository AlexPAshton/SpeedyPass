using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SpeedyPass.Views
{
    public partial class SetPinView : Window, ISetPinView
    {
        private ISetPinViewModel viewModel;
        private ISetPinViewController controller;

        public SetPinView()
        {
            this.InitializeComponent();
        }

        public void BindController(ISetPinViewController controller) => this.controller = controller;

        public void BindViewModel(ref ISetPinViewModel viewModel)
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

        private void Input0_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.Input0.Text.Length == 1)
            {
                this.Input1.Focus();
            }
        }

        private void Input1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.Input1.Text.Length == 1)
            {
                this.Input2.Focus();
            }
        }

        private void Input2_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.Input2.Text.Length == 1)
            {
                this.Input3.Focus();
            }
        }
    }
}
