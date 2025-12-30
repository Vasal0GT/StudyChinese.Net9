using LiteDB;
using StudyChinese._9plus.Net.LearnCards.CardsBack;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for LearnCardMenu.xaml
    /// </summary>
    public partial class LearnCardMenu : Window
    {
        static readonly string localDir = AppDomain.CurrentDomain.BaseDirectory;
        public LearnCardMenu()
        {
            InitializeComponent();
            CreateAndCheckDB();
            AddExampleCollections();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private static void CreateAndCheckDB()
        {
            //StudyCnDB.db
            if (File.Exists(localDir +  "StudyCnDB.db"))
            {
                return;
            }
            else
            {
                new LiteDatabase(localDir +  "StudyCnDB.db");
            }
        }
        public static void AddExampleCollections()
        {
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                if (!db.CollectionExists("CardBundles"))
                {
                    var cardBunlesCollection = db.GetCollection<CardsBundle>("CardBundles");

                    var HSK1 = new CardsBundle("HSK1", 100, 0);
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus1", "Chn", 1));
                    cardBunlesCollection.Insert(HSK1);

                    var HSK2 = new CardsBundle("HSK2", 200, 0);
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus2", "Chn", 1));
                    cardBunlesCollection.Insert(HSK2);

                    var HSK3 = new CardsBundle("HSK2", 300, 0);
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    HSK1.Cards.Add(new Card("Rus3", "Chn", 1));
                    cardBunlesCollection.Insert(HSK2);
                }
            }
        }

    }
}
