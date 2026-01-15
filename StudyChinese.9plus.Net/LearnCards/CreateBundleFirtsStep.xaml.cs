using LiteDB;
using StudyChinese._9plus.Net.LearnCards.CardsBack;
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

namespace StudyChinese._9plus.Net.LearnCards
{
    /// <summary>
    /// Interaction logic for CreateBundleFirtsStep.xaml
    /// </summary>
    public partial class CreateBundleFirtsStep : Window
    {
        static readonly string localDir = AppDomain.CurrentDomain.BaseDirectory;
        public CreateBundleFirtsStep()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LearnCardMenu learnCardMenu = new LearnCardMenu();
            learnCardMenu.Show();
            this.Close();
        }

        private void CreateCardBundle_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                var cardBunlesCollection = db.GetCollection<CardsBundle>("CardBundles");

                var newCardsBundle = new CardsBundle(SetNameTextBox.Text, 0, 0);
                cardBunlesCollection.Insert(newCardsBundle);
            }
            
            CreateCardForBundle createCardForBundle = new CreateCardForBundle(SetNameTextBox.Text);
            createCardForBundle.Show();
            this.Close();

        }
    }
}
