using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cryptographer
{
    /// <summary>
    /// Interaction logic for LoginOrRegisterWindow.xaml
    /// </summary>
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
