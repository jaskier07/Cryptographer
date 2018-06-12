using Cryptographer;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Hakerszyfr
{
    public partial class Decipher : Window
    {
        private String filename;
        private String filepath;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

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
                return;
            }

            string selectedUserEmail;
            try
            {
                selectedUserEmail = usersBox.SelectedItem.ToString().Split(' ')[1];
            }
            catch (Exception) { return; }

            if (selectedUserEmail != UsersControler.currentUser.email)
            {
                var x = usersBox.SelectedItem.ToString();
                var y = UsersControler.currentUser.email;
                return;
            }

            string encryptedFileContent = File.ReadAllText(filepath);
            string XMLStringMetadata = encryptedFileContent.Split(new[] { "</EncryptedFileHeader>" }, StringSplitOptions.None)[0] + "</EncryptedFileHeader>";
            string encyptedData = encryptedFileContent.Split(new[] { "</EncryptedFileHeader>" }, StringSplitOptions.None)[1];
            string sessionKey = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLStringMetadata);

            byte[] IV = Convert.FromBase64String(doc.SelectSingleNode("EncryptedFileHeader/IV").InnerText);

            CipherMode encryptionMode = 0;
            string cipherMode = doc.SelectSingleNode("EncryptedFileHeader/CipherMode").InnerText;
            if (cipherMode == "ECB")
                encryptionMode = CipherMode.ECB;
            if (cipherMode == "CBC")
                encryptionMode = CipherMode.CBC;
            if (cipherMode == "CFB")
                encryptionMode = CipherMode.CFB;

            XmlNodeList approvedUserNodes = doc.SelectNodes("/EncryptedFileHeader/ApprovedUsers");
            foreach (XmlNode node in approvedUserNodes)
            {
                if (UsersControler.currentUser.email == node.SelectSingleNode("User/Email").InnerText)
                {
                    string encryptedSessionKey = node.SelectSingleNode("User/SessionKey").InnerText;
                    sessionKey = Decrypt(encryptedSessionKey, UsersControler.currentUser.rsaPublicPrivateKey);
                    break;
                }

            }
            if (sessionKey == null)
            {
                MessageBox.Show("No user fitting to detailed in XML metadata");
                return;
            }
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = filename.Remove(filename.Length - 4) ;
            bool? safeDialogResult = sf.ShowDialog();

            if (safeDialogResult == true)
            {
                File.WriteAllText(Path.GetFullPath(sf.FileName), AESCryptography.DecryptStringFromBytes(Convert.FromBase64String(encyptedData), Convert.FromBase64String(sessionKey), IV, encryptionMode));
            }
        }

        public static string Decrypt(string data, string privateKey)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return _encoder.GetString(decryptedByte);
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