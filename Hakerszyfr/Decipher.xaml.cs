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
    /// Interaction logic for Decipher.xaml
    /// </summary>
    public partial class Decipher : Window
    {
        private String filename;
        private String filepath;

        public Decipher()
        {
            InitializeComponent();
        }

        private void openEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            encryptionWindow.Show();
            this.Hide();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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

        private void StartDecoding(Object sender, RoutedEventArgs e)
        {
            if (filename == null)
            {
                MessageBox.Show("No file chosen");
            }
        }
    }
}
