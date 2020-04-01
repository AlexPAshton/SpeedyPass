using SpeedyPass.Controllers;
using SpeedyPass.Models;
using SpeedyPass.Services;
using System;
using System.Collections.Generic;
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
        private MainWindowController controller;
        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            this.InitializeComponent();

            this.ConfigureNotifyIcon();
        }

        public void BindController(MainWindowController controller)
        {
            this.controller = controller;
        }

        private void ConfigureNotifyIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Click += NotifyIcon_Click;
            this.notifyIcon.Icon = new Icon(@"./Data/keyicon.ico");
            this.notifyIcon.Text = "SpeedyPass";
            this.notifyIcon.Visible = true;
        }

        public void BindViewModel(MainWindowViewModel viewModel)
        {
            Dispatcher.Invoke(() =>
            {
                this.DataContext = null;
                this.DataContext = viewModel;
            });
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
            this.controller.ShowAddPasswordDataDialog();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as System.Windows.Controls.ListBox).SelectedItem != null)
            {
                this.controller.DeletePasswordData(((sender as System.Windows.Controls.ListBox).SelectedItem as PasswordDataModel).Domain);
            }
        }
    }
}
