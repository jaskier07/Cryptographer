using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hakerszyfr
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Encryption : Window
    {
        private String filename;
        private String filepath;

        public Encryption()
        {
            InitializeComponent();
        }

        private void openFile(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.SafeFileName;
                filepath = dlg.FileName;
                chosenFileLabel.Content = filename;
            }
        }

        private void StartEncoding(Object sender, RoutedEventArgs e)
        {
            if (filename == null)
            {
               MessageBox.Show("No file chosen", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void openDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Decipher decipherWindow = new Decipher();
            this.Hide();
            decipherWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
