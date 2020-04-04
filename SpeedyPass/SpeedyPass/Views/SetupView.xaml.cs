using System.Windows;
using System.Windows.Input;

namespace SpeedyPass.Views
{
    public partial class SetupView : Window, ISetupView
    {
        private ISetupViewModel viewModel;
        private ISetupViewController controller;

        public SetupView()
        {
            this.InitializeComponent();
        }

        public void BindController(ISetupViewController controller) => this.controller = controller;

        public void BindViewModel(ref ISetupViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.OnVerifyInput += this.SetupWindowViewModel_OnVerifyInput;

            base.DataContext = this.viewModel;
        }

        private void SetupWindowViewModel_OnVerifyInput() => this.controller.VerifyInput();

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
    }
}
