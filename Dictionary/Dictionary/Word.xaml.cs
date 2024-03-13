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
using static System.Net.Mime.MediaTypeNames;

namespace Dictionary
{
    public partial class Word : Page
    {
        private WordDefinition word;
        public Word(WordDefinition searchWord)
        {
            InitializeComponent();
            word = searchWord;
        }
        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            /*empty*/
        }
        private void WordLoaded(object sender, RoutedEventArgs e)
        {
            name.Text=word.Name;
            category.Text=word.Category;
            description.Text=word.Description;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = System.IO.Path.Combine(baseDirectory, "..\\..\\..\\");
            string imagePath = System.IO.Path.Combine(directoryPath, "Resources", "Images", $"{word.Name}.jpg");
            if (File.Exists(imagePath))
            {
                photo.Source = new BitmapImage(new Uri(imagePath));
            }
            else
            {
                string defaultImagePath = System.IO.Path.Combine(directoryPath, "Resources", "Images", "default.jpg");
                photo.Source = new BitmapImage(new Uri(defaultImagePath));
            }
        }
    }
}
