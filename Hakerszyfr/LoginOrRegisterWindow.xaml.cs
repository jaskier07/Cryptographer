using Hakerszyfr;
using System;
using System.IO;
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

            foreach (string line in usersData)
            {
                String[] userData = line.Split('|'); // email, password, publicKey, privateKey    
                if (newEmail == userData[0])
                {
                    // TODO Textbox - This email already is in use
                }
                else
                {
                    String publicKey =null; // TODO generate
                    String privateKey = null; // TODO generate

                    var newUser = new User(newEmail, newPassword, publicKey, privateKey);
                    var newTxtLine = newEmail + "|" + newPassword + "|" + publicKey + "|" + privateKey;
                                        File.AppendAllText(@"Users login details.txt", Environment.NewLine + newTxtLine);

                    //using (StreamWriter w = File.AppendText("Users login details.txt"))
                   // {
                     //   w.WriteLine(newTxtLine);
                   // }
                    UsersControler.usersList.Add(newUser);
                    UsersControler.currentUser = newUser;
                    Close();
                    break;
                }
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
                    UsersControler.currentUser = new User(userData[0], userData[1], userData[2], userData[3]);
                    Close();
                }
            }

        }
    }
}
