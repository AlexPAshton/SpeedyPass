using CustomUnity;
using SpeedyPass.ViewModels;
using SpeedyPass.Views;
using System.Windows;

namespace SpeedyPass.Controllers
{
    public class DoneViewController : IDoneViewController
    {
        private IDoneView view;
        private IDoneViewModel viewModel;

        public DoneViewController()
        {
            this.view = CustomUnityContainer.Resolve<IDoneView>();
            this.viewModel = CustomUnityContainer.Resolve<IDoneViewModel>();

            this.view.BindViewModel(ref this.viewModel);
            this.view.BindController(this);

            this.view.Show();
        }

        public void ContinueClicked()
        {
            //Not implemented main window

            this.view.Close();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
