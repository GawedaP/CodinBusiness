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
    /// Interaction logic for EntryFormTypeOfOffice.xaml
    /// </summary>
    public partial class EntryFormTypeOfOffice : Window
    {
        TypesOfOffices typesOfOffices = new TypesOfOffices();

        public EntryFormTypeOfOffice()
        {
            InitializeComponent();
            this.DataContext = typesOfOffices;
        }

        public EntryFormTypeOfOffice(TypesOfOffices _typesOfOffices)
        {
            InitializeComponent();
            this.DataContext = _typesOfOffices;
            this.textBoxTOOfficeID.Text = _typesOfOffices.TOOfficeID.ToString();
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
