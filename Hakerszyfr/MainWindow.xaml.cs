using Cryptographer;
using System;
using System.Windows;

namespace Hakerszyfr
{
    public partial class MainWindow : Window
    {
        private void OpenEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            encryptionWindow.Show();
            Close();
        }

        private void OpenDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Decipher decipherWindow = new Decipher();
            decipherWindow.Show();
            Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            UsersControler userscontroler = new UsersControler();
            LoginOrRegisterWindow identyficationWindow = new LoginOrRegisterWindow();
            identyficationWindow.ShowDialog();
        }
    }
}
