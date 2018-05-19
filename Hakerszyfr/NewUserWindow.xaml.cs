using System.Windows;
using Hakerszyfr;

namespace Cryptographer
{
    public partial class NewUserWindow : Window
    {
        Encryption enWin;
     
        public NewUserWindow()
        {
            InitializeComponent(); 
        }

        public NewUserWindow(Encryption en)
        {
            enWin = en;
            InitializeComponent();
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            User user = new User(password.Text, email.Text);
            MainWindow.users.Add(user);

            Close();
        }
    }
}