using LiteDB;
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
using StudyChinese._9plus.Net.LearnCards.CardsBack;

namespace StudyChinese._9plus.Net.LearnCards
{
    /// <summary>
    /// Interaction logic for CreateCardForBundle.xaml
    /// </summary>
    public partial class CreateCardForBundle : Window
    {
        string localDir = AppDomain.CurrentDomain.BaseDirectory;
        static string _collectionName = string.Empty;
        public CreateCardForBundle(string collectionName)
        {
            _collectionName = collectionName;
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LearnCardMenu learnMenu = new LearnCardMenu();
            learnMenu.Show();
            this.Close();
        }

        private void SaveCard_Click(object sender, RoutedEventArgs e)
        {
            Card card = new Card(SetRusTextBox.Text, SetChnTextBox.Text, 0);
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                if (db.CollectionExists("CardBundles"))
                {
                    var cardBunlesCollection = db.GetCollection<CardsBundle>("CardBundles");
                    var currentBundle = cardBunlesCollection.FindOne(x => x.Name == _collectionName);
                    Console.WriteLine(currentBundle);
                    if (currentBundle != null && currentBundle is CardsBundle)
                    {
                        currentBundle.LeftWords++;
                        currentBundle.NumberOfWords++;
                        currentBundle.Cards.Add(card);
                    }
                    cardBunlesCollection.Update(currentBundle);
                }
            }
            CreateCardForBundle createCardForBundle = new CreateCardForBundle(_collectionName);
            createCardForBundle.Show();
            this.Close();
        }
    }
}
