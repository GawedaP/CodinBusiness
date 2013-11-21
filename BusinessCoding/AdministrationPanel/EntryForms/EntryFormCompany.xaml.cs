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
    /// Interaction logic for EntryFormCompany.xaml
    /// </summary>
    public partial class EntryFormCompany : Window
    {
        Companies company = new Companies();

        public EntryFormCompany()
        {
            InitializeComponent();
            this.DataContext = company;
        }

        public EntryFormCompany(Companies _companies)
        {
            InitializeComponent();
            this.DataContext = _companies;
            this.textBoxCompanyID.Text = _companies.CompanyID.ToString();
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
