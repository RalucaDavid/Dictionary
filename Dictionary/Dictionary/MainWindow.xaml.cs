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
using Dictionary;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordsManager data = new WordsManager();
        public MainWindow()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 520;
            data.LoadWords("C:\\Users\\Raluca David\\Desktop\\Portofoliu\\Dictionary\\Dictionary\\Dictionary\\Resources\\Data\\Words.txt");
            MainFrame.Navigate(new Search(data));
        }
    }
}
