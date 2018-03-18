using System.Windows;
using System.Windows.Controls;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            User user = new User(password.Text, email.Text);
            MainWindow.users.Add(user);
            enWin.usersListView.Items.Add(user);
            Close();
        }
    }
}