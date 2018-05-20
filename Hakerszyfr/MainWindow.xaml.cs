using Cryptographer;
using System;
using System.Windows;

namespace Hakerszyfr
{
    public partial class MainWindow : Window
    {
        public static Encryption encryptionWindow;
        public static Decipher decriptionWindow;

        private void OpenEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            encryptionWindow.Show();
        }

        private void OpenDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Decipher decipherWindow = new Decipher();
            decipherWindow.Show();
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
