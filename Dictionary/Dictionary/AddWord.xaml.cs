using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string selectedFilePath;
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
            if ((String.IsNullOrEmpty(name))|| (String.IsNullOrEmpty(description))||(categoryComboBox.SelectedIndex == -1))
            {
                MessageBox.Show("A field is empty.");
                return;
            }
            if(data.SearchWordByName(name)!=null)
            {
                MessageBox.Show("The word already exists.");
                return;
            }
            string category = (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string pattern = @"^[a-zA-Z\-]+$";
            if (!Regex.IsMatch(name, pattern))
            {
                MessageBox.Show("The word is not suitable for the dictionary.");
                return;
            }
            if (!String.IsNullOrEmpty(selectedFilePath))
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string directoryPath = System.IO.Path.Combine(baseDirectory, "..\\..\\..\\");
                string destinationFolderPath = System.IO.Path.Combine(directoryPath, "Resources", "Images");
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, name + System.IO.Path.GetExtension(selectedFilePath));
                File.Copy(selectedFilePath, destinationFilePath);
            }
            data.AddWord(name,category,description);
            data.SaveWords();
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                MessageBox.Show("The word was successfully added.");
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
        private void AddPhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
            }
        }
    }
}
