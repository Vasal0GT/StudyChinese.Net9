using StudyChinese._9plus.Net.QuizBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudyChinese._9plus.Net.QuizWindows
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private QuestionDTO _questionDTO;


        public QuestionWindow(QuestionDTO questionDTO)
        {
            InitializeComponent();
            _questionDTO = questionDTO;
            Loaded += QuestionWindow_Loaded;
        }

        private async void QuestionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await loadGuess();
        }

        private async Task loadGuess()
        {
            QuestionText.Text = _questionDTO.Guess;
        }

        private void RevealButton_Click(object sender, RoutedEventArgs e)
        {
            if (RevealButton.Content == "Вернуться к викторине")
            {
                this.DialogResult = true; 
                this.Close();
                return;
            }

            AnswerText.Text = _questionDTO.Answer;
            AnswerText.Opacity = 1;
            RevealButton.Content = "Вернуться к викторине";
        }
    }
}
