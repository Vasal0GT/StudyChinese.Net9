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
        string choosenDifficult = "HSK1";
        private readonly DeepSeekClient client = new DeepSeekClient("sk-b4c77c0ada764ccc8908dddf48a324ad");
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
                string choosenDifficult = current.Content?.ToString() ?? string.Empty;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = choosenDifficult;
                    ChatWindow.Children.Add(tb);
                    testField.Text = choosenDifficult;

                    ScrollViewer scrollViewer = FindParent<ScrollViewer>(ChatWindow);
                    scrollViewer?.ScrollToEnd();
                });
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
                TextBlock tb = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                tb.Text = userMessageField.Text;
                ChatWindow.Children.Add(tb);
                await SendToDeepSeekMessageAndRecieveAnswer(userMessageField.Text);
            });
        }
        private async Task SendToDeepSeekMessageAndRecieveAnswer(string userMessage)
        {
                    var request = new ChatRequest
                    {
                        Messages = [
                            DeepSeek.Core.Models.Message.NewSystemMessage("You are an AI assistant"),
                        DeepSeek.Core.Models.Message.NewUserMessage($"{userMessage}")
                            ],
                        Temperature = 1d
                    };
                    var chatResponse = await client.ChatAsync(request, new CancellationToken());
                    if (chatResponse is null)
                    {
                        ChatWindow.Children.Add(new TextBlock { Text =  client.ErrorMsg });
                    }
                    ChatWindow.Children.Add(new TextBlock { Text  = (chatResponse?.Choices.First().Message?.Content) });
        }
    }
}
