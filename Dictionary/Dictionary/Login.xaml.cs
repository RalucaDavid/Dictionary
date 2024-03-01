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
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private AuthenticationManager authManager;
        public Login()
        {
            InitializeComponent();
            authManager = new AuthenticationManager("C:\\Users\\Raluca David\\Desktop\\Portofoliu\\Dictionary\\Dictionary\\Dictionary\\Resources\\Data\\Accounts.txt");
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
                    navigationService.Navigate(new Uri("Admin.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("Authentication failed. Check your username and password.");
            }
        }
    }
}
