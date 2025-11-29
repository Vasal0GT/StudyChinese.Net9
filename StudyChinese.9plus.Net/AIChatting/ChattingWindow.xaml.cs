using DeepSeek.Core;
using DeepSeek.Core.Models;
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

namespace StudyChinese._9plus.Net.AIChatting
{
    /// <summary>
    /// Interaction logic for ChattingWindow.xaml
    /// </summary>
    public partial class ChattingWindow : Window
    {
        static string choosenDifficult = "HSK1";
        private readonly DeepSeekClient client = new DeepSeekClient("sk-b4c77c0ada764ccc8908dddf48a324ad");
        static string teacherPreSet = $"\"\r\nYou are my Chinese tutor. \r\nAlways answer **only in Chinese**.\r\n\r\nUse the difficulty level specified below:\r\n\r\nRules:\r\n- Beginner:\r\n    - Use only HSK1–HSK2 vocabulary\r\n    - Use very short sentences (5–8 characters)\r\n    - No idioms, no complex grammar\r\n- Intermediate:\r\n    - Use HSK3–HSK4 vocabulary\r\n    - Use medium sentences (10–15 characters)\r\n    - Can use basic grammar patterns and simple explanations\r\n- Advanced:\r\n    - Use HSK5–HSK6 vocabulary\r\n    - Use long sentences (15–30+ characters)\r\n    - Can use 成语, literary expressions and complex grammar\r\n Very Important use that Difficulty:";
        static string friendPreSet = "You just a Chinese friend of mine, speaks only Chinese Landuage Difficulty is:";
        string choosenPreset = teacherPreSet;
        public ChattingWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton current)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(current);

                // Ищем ближайший Panel
                while (parent != null && parent is not Panel)
                    parent = VisualTreeHelper.GetParent(parent);

                if (parent is Panel panel)
                {
                    foreach (var child in panel.Children)
                    {
                        if (child is ToggleButton btn && btn != current)
                            btn.IsChecked = false; // снять выбор
                    }
                }
                if (current.Content?.ToString() != "Teacher" && current.Content?.ToString() != "Friend")
                {
                    choosenDifficult = current.Content?.ToString() ?? string.Empty;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Border br = new Border
                        {
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness(0),
                            Margin = new Thickness(600, 10, 600, 10),
                            CornerRadius = new CornerRadius(20),
                            Background = new SolidColorBrush(Colors.LightGray)
                        };
                        TextBlock tb = new TextBlock();
                        tb.Text = choosenDifficult;
                        tb.FontSize = 22;
                        tb.HorizontalAlignment = HorizontalAlignment.Center;
                        br.Child = tb;
                        ChatWindow.Children.Add(br);

                        ScrollViewer scrollViewer = FindParent<ScrollViewer>(ChatWindow);
                        scrollViewer?.ScrollToEnd();
                    });
                } else 
                if (current.Content?.ToString() == "Teacher" || current.Content?.ToString() == "Friend")
                {
                    choosenPreset = current.Content?.ToString();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Border br = new Border
                        {
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness(0),
                            Margin = new Thickness(600, 10, 600, 10),
                            CornerRadius = new CornerRadius(20),
                            Background = new SolidColorBrush(Colors.LightGray)
                        };
                        TextBlock tb = new TextBlock();
                        tb.Text = choosenPreset;
                        tb.FontSize = 22;
                        tb.HorizontalAlignment = HorizontalAlignment.Center;
                        br.Child = tb;
                        ChatWindow.Children.Add(br);

                        ScrollViewer scrollViewer = FindParent<ScrollViewer>(ChatWindow);
                        scrollViewer?.ScrollToEnd();
                    });
                    if (choosenPreset == "Teacher")
                    { 
                        choosenPreset = teacherPreSet.ToString();
                    }
                    if (choosenPreset == "Friend")
                    {
                        choosenPreset = friendPreSet.ToString();
                    }
                }
                   
            }

        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            T parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void UserMessageSend_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                Border br = new Border
                {
                    BorderBrush = Brushes.Blue,
                    BorderThickness = new Thickness(0.5),
                    CornerRadius = new CornerRadius(20),
                    Background = new SolidColorBrush(Colors.LightBlue),
                    Margin = new Thickness(25),
                    Width = 350,
                    HorizontalAlignment = HorizontalAlignment.Right,
                };
                TextBlock tb = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 400,
                    Margin = new Thickness(5),
                    FontSize = 30
                };
                tb.Text = userMessageField.Text;
                br.Child = tb;
                ChatWindow.Children.Add(br);
                await SendToDeepSeekMessageAndRecieveAnswer(userMessageField.Text);
            });
        }
        private async Task SendToDeepSeekMessageAndRecieveAnswer(string userMessage)
        {
            string test = choosenPreset += choosenDifficult;
            var request = new ChatRequest
            {
                Messages = [
                    DeepSeek.Core.Models.Message.NewSystemMessage(choosenPreset += choosenDifficult),
        DeepSeek.Core.Models.Message.NewUserMessage($"{userMessage}")
                ],
                Temperature = 1d,
                MaxTokens = 300
            };

            var chatResponse = await client.ChatAsync(request, new CancellationToken());
                    if (chatResponse is null)
                    {
                        ChatWindow.Children.Add(new TextBlock { Text =  client.ErrorMsg });
                    }
            Border br = new Border
            {
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(0.5),
                CornerRadius = new CornerRadius(20),
                Background = new SolidColorBrush(Colors.LightPink),
                Margin = new Thickness(25),
                Width = 350,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            TextBlock tb = new TextBlock
            {
                Text  = (chatResponse?.Choices.First().Message?.Content),
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 400,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 30
            };
            br.Child = tb;
            ChatWindow.Children.Add(br);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void SendButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UserMessageSend_Click(sender, e);
            }
        }
    }
}
