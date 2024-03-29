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
using System.IO;

namespace Dictionary
{
    public partial class DeleteWord : Page
    {
        private WordsManager data = new WordsManager();
        public DeleteWord()
        {
            InitializeComponent();
            data.LoadWords();
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
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (data.RemoveWord(searchTextBox.Text))
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string directoryPath = System.IO.Path.Combine(baseDirectory, "..\\..\\..\\");
                string imagePath = System.IO.Path.Combine(directoryPath, "Resources", "Images", $"{searchTextBox.Text.Trim()}.jpg");
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                data.SaveWords();
                searchTextBox.Text = String.Empty;
                suggestionsListBox.Items.Clear();
                MessageBox.Show("The word was successfully deleted.");
            }
            else
            {
                MessageBox.Show("The word does not exist in the dictionary.");
            }
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.RemoveBackEntry();
                navigationService.Navigate(new Uri("../Pages/Admin.xaml", UriKind.Relative));
            }
        }
    }
}
