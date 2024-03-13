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
using System.Xml.Linq;
using System.IO;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for Round.xaml
    /// </summary>
    public partial class Round : Page
    {
        private WordsManager data = new WordsManager();
        private string choosenWord;
        private int numberRound, numberAnswers;
        public Round()
        {
            InitializeComponent();
            data.LoadWords();
            numberRound = 1;
            numberAnswers= 0;
        }
        public Round(int numRound,int numAnswers)
        {
            InitializeComponent();
            data.LoadWords();
            numberRound = numRound;
            numberAnswers = numAnswers;
        }
        private void RoundLoaded(object sender, RoutedEventArgs e)
        {
            WordDefinition word = data.ChooseRandomWord();
            choosenWord = word.Name;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = System.IO.Path.Combine(baseDirectory, "..\\..\\..\\");
            string imagePath = System.IO.Path.Combine(directoryPath, "Resources", "Images", $"{choosenWord}.jpg");
            if (File.Exists(imagePath))
            {
                Random random = new Random();
                int number = random.Next(1, 2);
                if(number == 1)
                    photo.Source = new BitmapImage(new Uri(imagePath));
                else
                    descriptionText.Text = word.Description;
            }
            else
            {
                descriptionText.Text = word.Description;
            }
            numberCorrect.Text = "Number of correct answers: " + numberAnswers;
            round.Text = "Round:" + numberRound;
            Finish.Visibility = Visibility.Collapsed;
        }
        private void VerifyAnswer(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (choosenWord == input.Text)
                {
                    numberAnswers++;
                    numberCorrect.Text = "Number of correct answers: " + numberRound.ToString();
                    MessageBox.Show("The answer is correct!");
                }
                else
                    MessageBox.Show("The answer isn't correct! The correct answer is " + choosenWord);
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if ((navigationService != null)&&(numberRound<5))
                {
                    numberRound++;
                    navigationService.Navigate(new Round(numberRound,numberAnswers));
                }
                if(numberRound==5)
                    Finish.Visibility = Visibility.Visible;
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new Uri("Entertainment.xaml", UriKind.Relative));
            }
        }
    }
}
