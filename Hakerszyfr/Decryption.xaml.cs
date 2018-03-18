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
            usersListView.ItemsSource = MainWindow.users;
        }

        private void openEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            Close();
            encryptionWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void StartDecrypting(Object sender, RoutedEventArgs e)
        {
            if (filename == null)
            {
                MessageBox.Show("No file chosen", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
