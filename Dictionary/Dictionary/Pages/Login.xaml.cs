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
using System.IO;

namespace Dictionary
{
    public partial class Login : Page
    {
        private AuthenticationManager authManager;
        public Login()
        {
            InitializeComponent();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = System.IO.Path.Combine(baseDirectory, "..\\..\\..\\");
            string dataPath = System.IO.Path.Combine(directoryPath, "Resources", "Data", "Accounts.txt");
            authManager = new AuthenticationManager(dataPath);
        }

        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            /*empty*/
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            if (authManager.CheckCredentials(username, password))
            {
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.RemoveBackEntry();
                    navigationService.Navigate(new Uri("../Pages/Admin.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("Authentication failed. Check your username and password.");
            }
        }
    }
}
