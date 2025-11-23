using StudyChinese._9plus.Net.QuizWindows;
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
using System.Windows.Shapes;

namespace StudyChinese._9plus.Net
{
    /// <summary>
    /// Interaction logic for QuizzMain.xaml
    /// </summary>
    public partial class QuizzMain : Window
    {
        public QuizzMain()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            ManagementWindow managementWindow = new ManagementWindow();
            managementWindow.Show();
            this.Close();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            QuizChoose quizChoose = new QuizChoose();
            quizChoose.Show();
            this.Close();
        }
    }
}
