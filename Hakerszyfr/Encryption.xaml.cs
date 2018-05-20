using Cryptographer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hakerszyfr
{
    public partial class Encryption : Window
    {
        private String filename;
        private String filepath;

        public Encryption()
        {
            InitializeComponent();
            LogOutButton.Content = UsersControler.currentUser.email;
            UpdateReceiversList();
            usersBox.SelectionMode = SelectionMode.Multiple;
        }

        private void AddNewUser(Object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(this);
            newUserWindow.Show();
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

        private User GetUserFromFile(string userName)
        {
            string[] usersData = System.IO.File.ReadAllLines(@"Users login details.txt");
            foreach (string line in usersData)
            {
                String[] userData = line.Split('|'); // email, password, RSAKey    
                if (userName == userData[0])
                    return new User(userData[0], userData[1], userData[2]);
            }
            return null;

        }

        private void StartEncoding(Object sender, RoutedEventArgs e)
        {
            if (filename == null)
            {
                MessageBox.Show("No file chosen", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<User> encryptionUsers = new List<User>();
            foreach (ListBoxItem userItem in usersBox.Items)
            {
                if (userItem.IsSelected)
                    encryptionUsers.Add(GetUserFromFile(userItem.Content.ToString()));
            }

            foreach (var user in encryptionUsers) // Encrypt data for selected users
            {
                // TODO
            }



            /*
             * 
             * 
             *                 try
                        {

                            string original = "Here is some data to encrypt!";

                            // Create a new instance of the RijndaelManaged 
                            // class.  This generates a new key and initialization  
                            // vector (IV). 
                            using (RijndaelManaged myRijndael = new RijndaelManaged())
                            {

                                myRijndael.GenerateKey();
                                myRijndael.GenerateIV();
                                // Encrypt the string to an array of bytes. 
                                byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

                                // Decrypt the bytes to a string. 
                                string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                                //Display the original data and the decrypted data.
                                Console.WriteLine("Original:   {0}", original);
                                Console.WriteLine("Round Trip: {0}", roundtrip);
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: {0}", e.Message);
                        }
                    }

            */

        }

        private void OpenDecipherWindow(Object sender, RoutedEventArgs e)
        {
            MainWindow.decriptionWindow = new Decipher();
            MainWindow.decriptionWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
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
                ListBoxItem itm = new ListBoxItem
                {
                    Content = userData[0]
                };
                usersBox.Items.Add(itm);
            }
        }
    }
}




