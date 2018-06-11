using Cryptographer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Hakerszyfr
{
    public partial class Encryption : Window
    {
        private String filename;
        private String filepath;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();
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
                    return new User(userData[0], userData[1], userData[2], userData[3]);
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

            CipherMode encryptionMode = 0;

            if ((bool)ECBRadioButton.IsChecked)
                encryptionMode = CipherMode.ECB;
            if ((bool)CBCRadioButton.IsChecked)
                encryptionMode = CipherMode.CBC;
            if ((bool)CFBRadioButton.IsChecked)
                encryptionMode = CipherMode.CFB;

            String fileContent = File.ReadAllText(filepath);
            if (encryptionMode == 0)
            {
                MessageBox.Show("You must choose encryption mode", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<User> encryptionUsers = new List<User>();
            foreach (ListBoxItem userItem in usersBox.Items)
            {
                if (userItem.IsSelected)
                    encryptionUsers.Add(GetUserFromFile(userItem.Content.ToString()));
            }

            if (encryptionUsers.Count == 0)
            {
                MessageBox.Show("No receiver choosen", "Cryptographer", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] generatedIV = null;
            byte[] generatedSessionKey = null;
            RSA sessionKey = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.GenerateKey();
                generatedSessionKey = myRijndael.Key;

                myRijndael.GenerateIV();
                generatedIV = myRijndael.IV;
            }

            var approvedUsers = new XElement("ApprovedUsers");
            foreach (var user in encryptionUsers)
            {
                var encryptedSessionKey = Encrypt(Convert.ToBase64String(generatedSessionKey), user.rsaPublicKey);
                approvedUsers.Add(new XElement("User",
                          new XElement("Email", user.email),
                          new XElement("SessionKey", encryptedSessionKey)
                          ));
            }

            var fileHeaders = new XElement("EncryptedFileHeader");
            fileHeaders.Add(new XElement("Algorithm", "AES"));
            fileHeaders.Add(new XElement("KeySize", "196")); // TODO is it OK?
            fileHeaders.Add(new XElement("BlockSize", "128"));
            fileHeaders.Add(new XElement("CipherMode", encryptionMode.ToString()));
            fileHeaders.Add(new XElement("IV", Convert.ToBase64String(generatedIV)));
            fileHeaders.Add(approvedUsers);

            var resultXMLFile = new XDocument(
                fileHeaders
                );

            resultXMLFile.Save(ResultFileNameBox.Text + ".xml"); // Path.GetExtension(filepath)

            // var temp = AESCryptography.EncryptStringToBytes(File.ReadAllText(filepath), generatedSessionKey, generatedIV, encryptionMode);
            // var temp1 = AESCryptography.DecryptStringFromBytes(temp, generatedSessionKey, generatedIV, encryptionMode);

            using (StreamWriter sw = File.AppendText(ResultFileNameBox.Text + ".xml"))
            {
                sw.Write(Convert.ToBase64String(AESCryptography.EncryptStringToBytes(File.ReadAllText(filepath), generatedSessionKey, generatedIV, encryptionMode)));
            }
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

            string[] usersData = File.ReadAllLines(@"Users login details.txt");
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

        public static string Encrypt(string data, string publicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = (Array)rsa.Encrypt(dataToEncrypt, false);
            var length = encryptedByteArray.Length;
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }
    }
}