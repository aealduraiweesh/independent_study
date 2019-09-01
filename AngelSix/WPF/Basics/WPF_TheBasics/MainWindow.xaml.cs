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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_TheBasics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckbox.IsChecked = RollCheckbox.IsChecked = SawCheckbox.IsChecked = false;
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            Length.Text += ((CheckBox)sender).Content.ToString();
        }

        private void FinishSlectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  this.Notes.Text = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
/*
            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;

            this.Notes.Text = (string)value.Content;*/

        }

        private void SupplierNameDataChanged(object sender, TextChangedEventArgs e)
        {
            Mass.Text = SupplierName.Text;
        }
    }
}
