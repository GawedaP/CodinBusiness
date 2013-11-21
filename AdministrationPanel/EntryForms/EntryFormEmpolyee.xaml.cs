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
    /// Interaction logic for EntryFormEmpolyee.xaml
    /// </summary>
    public partial class EntryFormEmpolyee : Window
    {
        Empolyees employees = new Empolyees();

        public EntryFormEmpolyee()
        {
            InitializeComponent();
            this.DataContext = employees;
        }

        public EntryFormEmpolyee(Empolyees _employees)
        {
            InitializeComponent();
            this.DataContext = _employees;
            this.textBoxEmployeeID.Text = _employees.EmployeeID.ToString();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textBoxDateOfHiring_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxDateOfHiring.Text.Length == 0)
            {
                textBoxDateOfHiring.Text = null;
            }
        }

        private void textBoxCompaniesCompanyID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxCompaniesCompanyID.Text.Length == 0)
            {
                textBoxCompaniesCompanyID.Text = null;
            }
        }
    }
}
