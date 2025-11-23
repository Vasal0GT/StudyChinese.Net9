using StudyChinese._9plus.Net.QuizBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudyChinese._9plus.Net.QuizWindows
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        public Table _table { get; set; }
        public Quiz(Table table)
        {
            _table = table;
            InitializeComponent();

            Loaded += Quiz_Loaded;
        }

        private async void Quiz_Loaded(object sender, RoutedEventArgs e)
        {
            await Load_name();
            await Create_Table();
        }
        private async Task Load_name()
        {
            QuizTitle.Text = _table.Name;
        }

        private async Task Create_Table()
        {
            GameGrid.ShowGridLines = false;
            GameGrid.Margin = new Thickness(20);
            for(int i = 0; i < _table.RowNumber; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i <= _table.ColumnNumber; i++)
            { 
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (var theme in _table.RowThemes)
            {
                TextBlock tb = new TextBlock
                {
                    Text = $"{theme.Value}",
                    Style = (Style)FindResource("ThemeTextStyle")
                };
                Grid.SetRow(tb, theme.Key);
                Grid.SetColumn(tb, 0);
                GameGrid.Children.Add(tb);
            }
            foreach(var question in _table.Questions)
            {
                Button bt = new Button
                {
                    Content = $"{10 * (_table.Multiplier * question.Column)}",
                    Tag = question,
                    Style = (Style)FindResource("QuestionButtonStyle")
                };
                bt.Click += OnQuestionClicked;

                Grid.SetColumn(bt, question.Column);
                Grid.SetRow(bt, question.Row);
                GameGrid.Children.Add(bt);
            }
        }
        private void OnQuestionClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button bt && bt.Tag is QuestionDTO question)
            { 
                QuestionWindow questionWindow = new QuestionWindow((QuestionDTO)bt.Tag);
                bool? result = questionWindow.ShowDialog();
                if (result == true)
                {
                    GameGrid.Children.Remove(bt);
                }
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            QuizChoose quizChoose = new QuizChoose();
            quizChoose.Show();
            this.Close();
        }
    }
}
