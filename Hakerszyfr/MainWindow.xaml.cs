using Cryptographer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Hakerszyfr
{
    public partial class MainWindow : Window
    {
        public static List<User> users = new List<User>();

        private void openEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            Hide();
            encryptionWindow.Show();
        }

        private void openDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Decipher decipherWindow = new Decipher();
            Hide();
            decipherWindow.Show();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
