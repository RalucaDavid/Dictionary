﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for Entertainment.xaml
    /// </summary>
    public partial class Entertainment : Page
    {
        public Entertainment()
        {
            InitializeComponent();
        }
        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            /*empty*/
        }
        private void StartClick(object sender, RoutedEventArgs e)
        {
           NavigationService navigationService = NavigationService.GetNavigationService(this);
           if (navigationService != null)
           {
                navigationService.Navigate(new Uri("Round.xaml", UriKind.Relative));
            }
        }
    }
}
