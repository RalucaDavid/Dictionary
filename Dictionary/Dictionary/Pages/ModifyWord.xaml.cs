﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ModifyWord.xaml
    /// </summary>
    public partial class ModifyWord : Page
    {
        private WordDefinition word;
        private WordsManager data = new WordsManager();
        private string selectedFilePath;
        public ModifyWord(WordDefinition modifyWord)
        {
            InitializeComponent();
            word = modifyWord;
            data.LoadWords();
        }
        private void ModifyWordLoaded(object sender, RoutedEventArgs e)
        {
            wordText.Text = word.Name;
            descriptionText.Text = word.Description;
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
        private void AddCategories(object sender, RoutedEventArgs e)
        {
            foreach (string category in data.Categories)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = category;
                categoryComboBox.Items.Add(item);
            }
            foreach (var item in categoryComboBox.Items)
            {
                if (item is ComboBoxItem comboBoxItem && comboBoxItem.Content.ToString() == word.Category)
                {
                    categoryComboBox.SelectedItem = comboBoxItem;
                    break;
                }
            }
        }
        private void AddPhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            data.RemoveWord(word.Name);
            string name = wordText.Text;
            string description = descriptionText.Text;
            if ((String.IsNullOrEmpty(name)) || (String.IsNullOrEmpty(description)) || (categoryComboBox.SelectedIndex == -1))
            {
                MessageBox.Show("A field is empty.");
                return;
            }
            string category = (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (data.AddWord(name, category, description))
            {
                data.AddImage(selectedFilePath, name);
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    MessageBox.Show("The word was successfully modified.");
                    navigationService.RemoveBackEntry();
                    navigationService.Navigate(new Uri("../Pages/Admin.xaml", UriKind.Relative));
                }
            }
        }

    }
}
