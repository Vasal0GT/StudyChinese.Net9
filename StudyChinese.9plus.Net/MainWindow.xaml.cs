using StudyChinese._9plus.Net.AIChatting;
using StudyChinese._9plus.Net.LearnCards;
using System.Windows;

namespace StudyChinese._9plus.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            QuizzMain quizzMain = new QuizzMain();
            quizzMain.Show();
            this.Close();
        }

        private void Button_Click_AIChatting(object sender, RoutedEventArgs e)
        {
            ChattingWindow chattingWindow = new ChattingWindow();
            chattingWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LearnCardMenu learnCards = new LearnCardMenu();
            learnCards.Show();
            this.Close();
        }
    }
}