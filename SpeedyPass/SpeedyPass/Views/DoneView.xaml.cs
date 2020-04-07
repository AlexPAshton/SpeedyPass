using SpeedyPass.Controllers;
using SpeedyPass.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SpeedyPass.Views
{
    public partial class DoneView : Window, IDoneView
    {
        private IDoneViewModel viewModel;
        private IDoneViewController controller;

        public DoneView()
        {
            this.InitializeComponent();
        }

        public void BindController(IDoneViewController controller) => this.controller = controller;

        public void BindViewModel(ref IDoneViewModel viewModel)
        {
            this.viewModel = viewModel;

            base.DataContext = this.viewModel;
        }

        private void CloseIcon_MouseDown(object sender, MouseButtonEventArgs e) => this.controller.CloseApplication();

        private void MinimizeText_MouseDown(object sender, MouseButtonEventArgs e) => this.Minimize();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => this.DragMoveWindow();

        private void NextButtonBackground_MouseDown(object sender, MouseButtonEventArgs e) => this.controller.ContinueClicked();

        private void DragMoveWindow()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Minimize()
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
