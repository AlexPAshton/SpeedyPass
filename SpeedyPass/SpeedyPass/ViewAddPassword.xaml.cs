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

namespace SpeedyPass
{
    public class ViewModelAddPassword
    {
        private PasswordDataModel passwordDataModel;

        public PasswordDataModel PasswordDataModel { get => passwordDataModel; set => passwordDataModel = value; }
    }

    public partial class ViewAddPassword : Window
    {
        public PasswordDataModel Result 
        {
            get 
            { 
                return this.viewModelAddPassword.PasswordDataModel;
            } 
        }

        private ViewModelAddPassword viewModelAddPassword;

        public ViewAddPassword()
        {
            this.InitializeComponent();

            this.viewModelAddPassword = new ViewModelAddPassword();
            this.viewModelAddPassword.PasswordDataModel = new PasswordDataModel();

            this.DataContext = this.viewModelAddPassword;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
