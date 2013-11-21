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
using AdministrationPanel.DatabaseEntityFramework;

namespace AdministrationPanel.EntryForms
{
    /// <summary>
    /// Interaction logic for EntryFormUser.xaml
    /// </summary>
    public partial class EntryFormUser : Window
    {
        Users users = new Users();

        public EntryFormUser()
        {
            InitializeComponent();
            this.DataContext = users;
        }

        public EntryFormUser(Users _user)
        {
            InitializeComponent();
            this.DataContext = _user;
            this.textBoxUserID.Text = _user.UserID.ToString();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
