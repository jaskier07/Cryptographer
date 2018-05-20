using Cryptographer;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hakerszyfr
{
    public partial class Decipher : Window
    {
        private String filename;
        private String filepath;

        public Decipher()
        {
            InitializeComponent();
            UpdateReceiversList();
            LogOutButton.Content = UsersControler.currentUser.email;
        }

        private void OpenEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            MainWindow.encryptionWindow = new Encryption();
            MainWindow.encryptionWindow.Show();
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

        private void ButtonOpenAuthenticationWindow(object sender, RoutedEventArgs e)
        {
            LoginOrRegisterWindow identyficationWindow = new LoginOrRegisterWindow();
            identyficationWindow.ShowDialog();
            LogOutButton.Content = UsersControler.currentUser.email;
        }
        public void UpdateReceiversList()
        {
            usersBox.Items.Clear();

            string[] usersData = System.IO.File.ReadAllLines(@"Users login details.txt");
            foreach (string line in usersData)
            {
                String[] userData = line.Split('|'); // email, password, publicKey, privateKey    
                ListBoxItem itm = new ListBoxItem();
                itm.Content = userData[0];
                usersBox.Items.Add(itm);
            }
        }
    }
}
