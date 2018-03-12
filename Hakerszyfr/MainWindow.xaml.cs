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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hakerszyfr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void openEncryptionWindow(Object sender, RoutedEventArgs e)
        {
            Encryption encryptionWindow = new Encryption();
            this.Hide();
            encryptionWindow.Show();
        }

        private void openDecipherWindow(Object sender, RoutedEventArgs e)
        {
            Decipher decipherWindow = new Decipher();
            this.Hide();
            decipherWindow.Show();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
