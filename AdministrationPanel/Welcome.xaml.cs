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

namespace AdministrationPanel
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private string login = "admin";
        private string password = "Admin123";
        
        MainWindow main;
        public Welcome(MainWindow mw)
        {
            main = mw;
            InitializeComponent();
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordBox.Password == password && textBoxLogin.Text == login)
            {
                this.Close();
                main.Show();
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == password && textBoxLogin.Text == login)
            {
                this.Close();
                main.Show();
            }
        }
    }
}
