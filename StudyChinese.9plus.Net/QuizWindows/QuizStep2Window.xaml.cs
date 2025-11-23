using StudyChinese._9plus.Net.QuizBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Path = System.IO.Path;
using Table = StudyChinese._9plus.Net.QuizBack.Models.Table;

namespace StudyChinese._9plus.Net.QuizWindows
{
    /// <summary>
    /// Interaction logic for QuizStep2Window.xaml
    /// </summary> 
    public partial class QuizStep2Window : Window
    {
        private readonly string quizFolder;
        private Border _selectedTheme;
        Table _table = new Table();
        private int questionCount = 0;
        private Dictionary<int, List<object>> themeTagQuestionsListPairs = new Dictionary<int, List<object>>();
        private Dictionary<int, string> themeNameTagConnection = new Dictionary<int, string>();
        public QuizStep2Window(Table table)
        {
            _table = table;
            InitializeComponent();

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            quizFolder = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\QuizBack\JsonTables"));

            Loaded += QuizStep2Window_Loaded;
        }
        private async void QuizStep2Window_Loaded(object sender, RoutedEventArgs e)
        {
            await AddThemes();
        }
        private async Task AddThemes()
        {
            QuizName.Text = $"Создание викторины: {_table.Name}";
            stackPanelWithThemes.Children.Clear();
            for (int i = 0; i < _table.RowNumber; i++)
            {
                Border br = new Border
                {
                    Style = (Style)FindResource("ThemeCard"),
                    Tag = i,
                };

                br.MouseLeftButtonDown += ThemeBorder_MouseLeftButtonDown; 

                StackPanel panel = new StackPanel();


                TextBox tx = new TextBox
                {
                    Text = $"Тема {i + 1}",
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333")),
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(0)
                };
                panel.Children.Add(tx);
                TextBlock tx2 = new TextBlock
                {
                    Text = "0 вопросов",
                    FontSize = 12,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"))
                };
                panel.Children.Add(tx2);
                br.Child = panel;

                stackPanelWithThemes.Children.Add(br);
            }
        }

        private void QuizStep2Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ThemeBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Сбрасываем прошлый выбор
            if (_selectedTheme != null)
            {
                _selectedTheme.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD2D2"));

                List<object> questionsList = new List<object>();
                foreach(var a in questionPanel.Children)
                    questionsList.Add(a);
                if (!themeTagQuestionsListPairs.Keys.Contains((int)_selectedTheme.Tag))
                {
                    themeTagQuestionsListPairs.Add((int)_selectedTheme.Tag, questionsList);
                }
                themeTagQuestionsListPairs[(int)_selectedTheme.Tag] = questionsList;
            }

                // Устанавливаем новую выбранную
            var border = sender as Border;
            border.Background = new SolidColorBrush(Color.FromRgb(200, 230, 255)); // голубоватая подсветка
            _selectedTheme = border;
            ChangeQueastionSection((int)_selectedTheme.Tag);
        }
        private void ChangeQueastionSection(int newTag)
        {
                questionPanel.Children.Clear();
                questionCount = 0;
            if (themeTagQuestionsListPairs.ContainsKey(newTag))
            {
                // Достаем список элементов для выбранной темы
                var questionsList = themeTagQuestionsListPairs[newTag];

                foreach (var obj in questionsList)
                {
                    if (obj is UIElement element)
                    {
                        questionPanel.Children.Add(element);
                        questionCount++;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            warringTextNoQuestions.Text = "";
            questionCount++;
            // Карточка вопроса
            Border questionCard = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFA2A2"),
                BorderThickness = new Thickness(2),
                Padding = new Thickness(15),
                Margin = new Thickness(0, 10, 0, 10),
                Effect = (Effect)this.FindResource("CardShadow"),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            // Основная структура — вертикальная
            Grid cardGrid = new Grid();
            cardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // заголовок с номером
            cardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // поля
            cardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // стрелки

            // Номер вопроса
            TextBlock questionNumber = new TextBlock
            {
                Text = $"№ {questionCount}",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)new BrushConverter().ConvertFrom("#B22222"),
                Margin = new Thickness(0, 0, 0, 10),
                Tag = questionCount
            };
            Grid.SetRow(questionNumber, 0);

            // Вопрос и ответ
            StackPanel contentPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center
            };

            TextBox questionTextBox = new TextBox
            {
                Style = (Style)this.FindResource("PinkTextBox"),
                Tag = "Введите текст вопроса...",
                Height = 80,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
            };

            TextBox answerTextBox = new TextBox
            {
                Style = (Style)this.FindResource("PinkTextBox"),
                Tag = "Введите ответ...",
                Height = 80,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Margin = new Thickness(0, 5, 0, 0),
            };

            contentPanel.Children.Add(questionTextBox);
            contentPanel.Children.Add(answerTextBox);
            Grid.SetRow(contentPanel, 1);

            // Стрелки управления (справа снизу)
            StackPanel arrowsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Button upButton = new Button
            {
                Content = "↑",
                Width = 30,
                Height = 30,
                Background = Brushes.Transparent,
                BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFA2A2"),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 5, 0),
                Cursor = Cursors.Hand
            };

            Button downButton = new Button
            {
                Content = "↓",
                Width = 30,
                Height = 30,
                Background = Brushes.Transparent,
                BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFA2A2"),
                BorderThickness = new Thickness(1),
                Cursor = Cursors.Hand
            }; var hoverBrush = (Brush)new BrushConverter().ConvertFrom("#FFFFE5E5");
            upButton.MouseEnter += (s, e2) => upButton.Background = hoverBrush;
            upButton.MouseLeave += (s, e2) => upButton.Background = Brushes.Transparent;
            downButton.MouseEnter += (s, e2) => downButton.Background = hoverBrush;
            downButton.MouseLeave += (s, e2) => downButton.Background = Brushes.Transparent;

            arrowsPanel.Children.Add(upButton);
            arrowsPanel.Children.Add(downButton);
            Grid.SetRow(arrowsPanel, 2);

            // Собираем всё
            cardGrid.Children.Add(questionNumber);
            cardGrid.Children.Add(contentPanel);
            cardGrid.Children.Add(arrowsPanel);

            questionCard.Child = cardGrid;
            questionPanel.Children.Add(questionCard);

            questionScrollViewer.ScrollToEnd();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            QuizStep1Window quizStep1Window = new QuizStep1Window();
            quizStep1Window.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTheme != null)
            {
                List<object> questionsList = new List<object>();
                foreach (var a in questionPanel.Children)
                    questionsList.Add(a);

                themeTagQuestionsListPairs[(int)_selectedTheme.Tag] = questionsList;
            }
            Table forSerialization = new Table();
            forSerialization.Name = _table.Name;
            forSerialization.Description = _table.Description;
            forSerialization.RowNumber = _table.RowNumber;
            forSerialization.Multiplier = _table.Multiplier;
            forSerialization.ColumnNumber = countColumnNumber();
            forSerialization.Questions = returnAllQuestionsList();
            forSerialization.RowThemes = returnRowThemes();
            string temp = Table.ConvertToJson(forSerialization, forSerialization.Name);
            if (temp == "Викторина была успешно сохранена")
            {
                QuizName.Foreground = Brushes.Green;
                QuizName.Text = temp;
            }
            else
            {
                QuizName.Foreground = Brushes.Red;
                QuizName.Text = temp;
            }
        }
        private int countColumnNumber()
        {
            if (themeTagQuestionsListPairs == null || themeTagQuestionsListPairs.Count == 0)
                return 1;

            int maxQuestions = themeTagQuestionsListPairs
                .Max(kv => kv.Value?.Count ?? 0);

            return Math.Max(1, maxQuestions);
        }
        private List<QuestionDTO> returnAllQuestionsList()
        {
            var result = new List<QuestionDTO>();

            Dispatcher.Invoke(() =>
            {
                foreach (var themeEntry in themeTagQuestionsListPairs)
                {
                    int row = themeEntry.Key;
                    var items = themeEntry.Value;
                    int column = 0;

                    foreach (var obj in items)
                    {
                        if (obj is Border card && card.Child is Grid grid)
                        {
                            var contentPanel = grid.Children.OfType<StackPanel>()
                                .FirstOrDefault();
                            if (contentPanel == null)
                                continue;

                            var textBoxes = contentPanel.Children.OfType<TextBox>().ToList();
                            if (textBoxes.Count < 2)
                                continue;

                            string guess = textBoxes[0].Text?.Trim();
                            string answer = textBoxes[1].Text?.Trim();

                            if (string.IsNullOrWhiteSpace(guess) &&
                                string.IsNullOrWhiteSpace(answer))
                                continue;

                            result.Add(new QuestionDTO(
                                guess,
                                answer,
                                column + 1,
                                row,
                                _table.Name
                            ));

                            column++;
                        }
                    }
                }
            });

            return result;
        }
        private Dictionary<int, string> returnRowThemes()
        {
            var result = new Dictionary<int, string>();

            Dispatcher.Invoke(() =>
            {
                foreach (Border themeCard in stackPanelWithThemes.Children)
                {
                    int row = (int)themeCard.Tag;

                    var panel = themeCard.Child as StackPanel;
                    if (panel == null)
                        continue;

                    var themeNameTextBox = panel.Children.OfType<TextBox>().FirstOrDefault();
                    if (themeNameTextBox == null)
                        continue;

                    string themeName = themeNameTextBox.Text?.Trim();
                    if (string.IsNullOrWhiteSpace(themeName))
                        themeName = $"Тема {row + 1}";

                    result[row] = themeName;
                }
            });

            return result;
        }
    }
}
