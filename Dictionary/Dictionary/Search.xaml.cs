using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Search : Page
    {
        private WordsManager data = new WordsManager();
        public Search()
        {
            InitializeComponent();
            data.LoadWords();
        }
        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            /*empty*/
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text;
            suggestionsListBox.Items.Clear();
            if (!string.IsNullOrEmpty(searchText))
            {
                foreach (WordDefinition word in data.WordsList)
                {
                    if (word.Name.StartsWith(searchText))
                    {
                        if(categoryComboBox.SelectedIndex == 0 || (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() == word.Category)
                            suggestionsListBox.Items.Add(word.Name);
                    }
                }
            }
            suggestionsListBox.Visibility = suggestionsListBox.HasItems ? Visibility.Visible : Visibility.Collapsed;
        }
        private void SuggestionsListBox_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (suggestionsListBox.SelectedItem != null)
            {
                searchTextBox.Text = suggestionsListBox.SelectedItem.ToString();
                suggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }
        private void AddCategories(object sender, RoutedEventArgs e)
        {
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.Add("none");
            foreach (string category in data.Categories)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = category;
                categoryComboBox.Items.Add(item);
            }
            categoryComboBox.SelectedIndex = 0;
        }
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                WordDefinition word = data.SearchWordByName(searchTextBox.Text);
                if (word != null)
                {
                    NavigationService navigationService = NavigationService.GetNavigationService(this);
                    if (navigationService != null)
                    {
                        navigationService.Navigate(new Word(word));
                    }
                }
                else
                {
                    MessageBox.Show("The word does not exist in the dictionary.");
                }
            }
        }
    }
}
