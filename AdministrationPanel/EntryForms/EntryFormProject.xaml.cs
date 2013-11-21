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
    /// Interaction logic for EntryFormProject.xaml
    /// </summary>
    public partial class EntryFormProject : Window
    {
        Projects projects = new Projects();

        public EntryFormProject()
        {
            InitializeComponent();
            this.DataContext = projects;
        }

        public EntryFormProject(Projects _projects)
        {
            InitializeComponent();
            this.DataContext = _projects;
            this.textBoxProjectID.Text = _projects.ProjectID.ToString();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textBoxTimeToEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxTimeToEnd.Text.Length == 0)
            {
                textBoxTimeToEnd.Text = null;
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
