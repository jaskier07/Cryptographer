using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;

namespace Hakerszyfr
{
    public partial class Decipher : Window
    {
        private String filename;
        private String filepath;

        public Decipher()
        {
            InitializeComponent();
        }

        private void OpenEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Hide();
            Encryption encryptionWindow = new Encryption();
            encryptionWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenFile(Object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.SafeFileName;
                filepath = dlg.FileName;
                chosenFileLabel.Content = filename;
            }
        }

        private void StartDecrypting(Object sender, RoutedEventArgs e)
        {
            if (filename == null)
            {
                MessageBox.Show("No file chosen", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewUser(Object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(Object sender, RoutedEventArgs e)
        {

        }
    }
}
