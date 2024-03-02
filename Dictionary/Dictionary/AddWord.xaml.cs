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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for AddWord.xaml
    /// </summary>
    public partial class AddWord : Page
    {
        private WordsManager data = new WordsManager();
        public AddWord()
        {
            InitializeComponent();
            data.LoadWords();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new Uri("Admin.xaml", UriKind.Relative));
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            string name = wordText.Text;
            string description = descriptionText.Text;
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new Uri("Admin.xaml", UriKind.Relative));
            }
        }
        private void AddCategories(object sender, RoutedEventArgs e)
        {
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.Add("Add category");
            foreach (string category in data.Categories)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = category;
                categoryComboBox.Items.Add(item);
            }
            categoryComboBox.SelectedIndex = -1;
        }
        private void categorySelection(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedItem != null && categoryComboBox.SelectedItem.ToString() == "Add category")
            {
                string newCategory = Microsoft.VisualBasic.Interaction.InputBox("Add the new category:", "Add category", "");
                if (!string.IsNullOrEmpty(newCategory))
                {
                    categoryComboBox.Items.Insert(categoryComboBox.Items.Count - 1, newCategory);
                    categoryComboBox.SelectedItem = newCategory;
                }
                else
                {
                    categoryComboBox.SelectedIndex = -1;
                }
            }
        }
    }
}
