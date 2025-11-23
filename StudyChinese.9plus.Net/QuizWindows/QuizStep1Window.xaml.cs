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
using System.Windows.Shapes;
using Table = StudyChinese._9plus.Net.QuizBack.Models.Table;

namespace StudyChinese._9plus.Net.QuizWindows
{
    /// <summary>
    /// Interaction logic for QuizStep1Window.xaml
    /// </summary>
    public partial class QuizStep1Window : Window
    {
        Table _table; 
        public QuizStep1Window()
        {
            InitializeComponent();
            _table = new Table();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(quizName.Text))
            {
                MessageBox.Show("Пожалуйста, введите текст!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                _table.Name = quizName.Text;
                _table.Description = quizDescription.Text;
                _table.RowNumber = int.Parse(themeNumber.Text);
                _table.Multiplier = int.Parse(quizMultiplier.Text);
            }
            );
            QuizStep2Window quizStep2Window = new QuizStep2Window(_table);
            quizStep2Window.Show();
            this.Close();
        }

        private void Int_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            e.Handled = rx.IsMatch(e.Text);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            QuizzMain quizzMain = new QuizzMain();
            quizzMain.Show();
            this.Close();
        }
    }
}
