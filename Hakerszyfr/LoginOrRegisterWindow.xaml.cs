using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;

namespace Cryptographer
{
    public partial class LoginOrRegisterWindow : Window
    {
        public LoginOrRegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterAction(object sender, RoutedEventArgs e)
        {
            String newEmail = RegisterEmailTextBox.Text;
            String newPassword = RegisterPasswordTextBox.Text;

            string[] usersData = System.IO.File.ReadAllLines(@"Users login details.txt");
            bool isRegisterAllowed = true;
            if (newPassword.Length >= 7 &&
                newPassword.Any(char.IsUpper) &&
                newPassword.Any(char.IsDigit))
            {
                foreach (string line in usersData)
                {
                    String[] userData = line.Split('|'); // email, password, publicKey, privateKey    
                    if (newEmail == userData[0])
                    {
                        isRegisterAllowed = false;
                        break;
                        // TODO Textbox - This email already is in use
                    }
                }

                if (isRegisterAllowed)
                {
                    RSA newRsaKeys = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key

                    var newUser = new User(newEmail, newPassword, newRsaKeys.ToXmlString(true));
                    var newTxtLine = newEmail + "|" + newPassword + "|" + newRsaKeys.ToXmlString(true);
                    File.AppendAllText(@"Users login details.txt", Environment.NewLine + newTxtLine);
                    UsersControler.usersList.Add(newUser);
                    UsersControler.currentUser = newUser;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Password is too weak, needs at lest one number, one great letter and 8 signs");
            }
        }

        private void LoginAction(object sender, RoutedEventArgs e)
        {
            string[] usersData = System.IO.File.ReadAllLines(@"Users login details.txt");

            foreach (string line in usersData)
            {
                String[] userData = line.Split('|'); // email, password, publicKey, privateKey    
                if (LoginEmailTextBox.Text == userData[0] && LoginPasswordTextBox.Text == userData[1])
                {
                    UsersControler.currentUser = new User(userData[0], userData[1], userData[2]);
                    Close();
                }
            }

        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        { //TODO
            if (UsersControler.currentUser == null)
            {
                Application.Current.Shutdown();
                Environment.Exit(0);

            }
            MessageBox.Show("XD");
            Application.Current.Shutdown();
            Environment.Exit(0);

        }
    }
}
