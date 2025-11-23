using Newtonsoft.Json;
using StudyChinese._9plus.Net.QuizBack.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace StudyChinese._9plus.Net.QuizWindows
{
    /// <summary>
    /// Interaction logic for QuizChoose.xaml
    /// </summary>
    public partial class QuizChoose : Window
    {
        private readonly string quizFolder;
        private List<Table> loadedQuizes = new List<Table>();
        public QuizChoose()
        {
            InitializeComponent();

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            quizFolder = Path.GetFullPath(Path.Combine(baseDir, @"QuizBack\JsonTables"));

            Loaded += QuizChoose_Loaded;
        }

        private void QuizList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (QuizList.SelectedItem is Table selectedTable)
            {
                Quiz quiz = new Quiz((Table)QuizList.SelectedItem);
                quiz.Show();
                this.Close();
            }
        }

        private async void QuizChoose_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadQuizzesAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            QuizzMain quizzMain = new QuizzMain();
            quizzMain.Show();
            this.Close();
        }

        private async Task LoadQuizzesAsync()
        {
            var files = Directory.GetFiles(quizFolder, "*.json");
            Array.Sort(files);

            var quizes = await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var file in files)
                    {
                        string json = File.ReadAllText(file);
                        var table = JsonConvert.DeserializeObject<Table>(json);
                        QuizList.DisplayMemberPath = "Name";
                        if (table != null) QuizList.Items.Add(table);
                    }
                });
                return QuizList;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz((Table)QuizList.SelectedItem);
            quiz.Show();
            this.Close();
        }
    }
}
