using Cryptographer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Hakerszyfr
{
    public partial class MainWindow : Window
    {
        public static List<User> users = new List<User>();
       
        private void OpenEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Hide();
            Encryption encryptionWindow = new Encryption();
            encryptionWindow.Show();
        }

        private void OpenDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Hide();
            Decipher decipherWindow = new Decipher();
            decipherWindow.Show();
        }

        public MainWindow()
        {
            InitializeComponent();

            LoginOrRegisterWindow identyficationWindow = new LoginOrRegisterWindow();
            identyficationWindow.ShowDialog();

            UsersControler userscontroler = new UsersControler();
        }
    }
}
