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
                    testField.Text = choosenDifficult;
                });
            }
        }
    }
}
