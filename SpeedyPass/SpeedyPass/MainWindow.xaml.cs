using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SpeedyPass
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        private NotifyIcon notifyIcon;
        private PasswordDataService passwordDataService;

        public MainWindow()
        {
            this.InitializeComponent();

            this.mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = this.mainWindowViewModel;

            this.notifyIcon = new NotifyIcon();
            this.passwordDataService = new PasswordDataService();

            this.notifyIcon.Click += NotifyIcon_Click;
            this.notifyIcon.Icon = new Icon(@"./keyicon.ico");
            this.notifyIcon.Text = "SpeedyPass";
            this.notifyIcon.Visible = true;


            this.mainWindowViewModel.VersionString = this.GetBuildNumber();
            this.mainWindowViewModel.PasswordDataModelList = this.passwordDataService.passwordDataModelList.Value;
        }

        private string GetBuildNumber()
        {
            return File.GetCreationTime("./SpeedyPass.exe").ToString("ddMMyyyy.HHmmss");
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = System.Windows.SystemParameters.WorkArea.Right - (this.Width + 10.0f);
            this.Top = System.Windows.SystemParameters.WorkArea.Bottom - this.Height;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            new Thread(() =>
            {
                Thread.Sleep(4000);
                if (!this.IsMouseOver)
                {
                   Dispatcher.Invoke(()=> this.Hide());
                }
            }).Start();
        }

        private void TextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Clipboard.SetText((sender as TextBlock).Text);
            this.FlashText((sender as TextBlock));
        }

        private void TextBox_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Clipboard.SetText((sender as TextBlock).Text);

            this.FlashText((sender as TextBlock));
        }

        private void FlashText(TextBlock tb)
        {
            System.Windows.Media.Brush sb = tb.Foreground;
            tb.Foreground = System.Windows.Media.Brushes.Green;
            new Thread(() =>
            {
                Thread.Sleep(100);
               Dispatcher.Invoke(()=> tb.Foreground = sb);
            }).Start();

        }

        private void AddLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewAddPassword viewAddPassword = new ViewAddPassword();
            viewAddPassword.ShowDialog();

            PasswordDataModel passwordDataModel = viewAddPassword.Result;

            if (passwordDataModel.Domain != null)
            {
                this.mainWindowViewModel.PasswordDataModelList.Add(passwordDataModel);
                this.passwordDataService.Save(this.mainWindowViewModel.PasswordDataModelList);

                this.DataContext = null;
                this.DataContext = this.mainWindowViewModel;
            }
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as System.Windows.Controls.ListBox).SelectedItem != null)
            {
                this.mainWindowViewModel.PasswordDataModelList.RemoveAll(s => s.Domain == ((sender as System.Windows.Controls.ListBox).SelectedItem as PasswordDataModel).Domain);

                this.DataContext = null;
                this.DataContext = this.mainWindowViewModel;
            }
        }

        private void ReloadLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.mainWindowViewModel.PasswordDataModelList = this.passwordDataService.Load();
        }
    }
}
