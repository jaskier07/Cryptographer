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
            Close();
        }

        private void LoginAction(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
