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
using System.ComponentModel;
using AdministrationPanel.DatabaseEntityFramework;

namespace AdministrationPanel.EntryForms
{
    /// <summary>
    /// Interaction logic for EntryFormTypeOfEmployee.xaml
    /// </summary>
    public partial class EntryFormTypeOfEmployee : Window
    {
        TypesOfEmployees typesOfEmployees = new TypesOfEmployees();

        public EntryFormTypeOfEmployee()
        {
            InitializeComponent();
            this.DataContext = typesOfEmployees;
        }

        public EntryFormTypeOfEmployee(TypesOfEmployees _typesOfEmployees)
        {
            InitializeComponent();
            this.DataContext = _typesOfEmployees;
            this.textBoxTOEmployeeID.Text = _typesOfEmployees.TOEmployeeID.ToString();
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
