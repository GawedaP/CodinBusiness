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
    /// Interaction logic for EntryFormDescription.xaml
    /// </summary>
    public partial class EntryFormDescription : Window
    {
        Descriptions descriptions = new Descriptions();
        public EntryFormDescription()
        {
            InitializeComponent();
            this.DataContext = descriptions;
        }

        public EntryFormDescription(Descriptions _descriptions)
        {
            InitializeComponent();
            this.DataContext = _descriptions;
            this.textBoxDescriptionID.Text = _descriptions.DescriptionID.ToString();
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
