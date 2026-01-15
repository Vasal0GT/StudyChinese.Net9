using LiteDB;
using Microsoft.Win32;
using StudyChinese._9plus.Net.LearnCards.CardsBack;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace StudyChinese._9plus.Net.LearnCards
{
    /// <summary>
    /// Interaction logic for LearnCardMenu.xaml
    /// </summary>
    public partial class LearnCardMenu : Window
    {
        static readonly string localDir = AppDomain.CurrentDomain.BaseDirectory;
        static string HandleSelectedFile;
        public ObservableCollection<CardsBundle> Items { get; } = new();
        public LearnCardMenu()
        {
            InitializeComponent();
            DataContext = this;
            CreateAndCheckDB();
            AddExampleCollections();
            Loaded +=  SavedBundles_Loaded;
        }
        private async void SavedBundles_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBundlesAsync();
        }
        private async Task LoadBundlesAsync()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                //тут типо каждый набор надо вытаскивать из бд и добавлять в Items
                using (var db = new LiteDatabase(localDir + "StudyCnDB.db"))
                {
                    var dataBase = db.GetCollection<CardsBundle>("CardBundles")
                    .Query()
                    .OrderBy(x => x.Name)
                    .ToList();

                    foreach (var a in dataBase)
                    {
                        Items.Add(a);
                    }

                }
            });
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
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
                if (db.CollectionExists("CardBundles"))
                {
                    var cardBunlesCollection = db.GetCollection<CardsBundle>("CardBundles");
                    cardBunlesCollection.DeleteAll();
                    if (cardBunlesCollection.Count() < 1)
                    {
                        var HSK1 = new CardsBundle("HSK1", 100, 0);
                        HSK1.Cards.Add(new Card("Привет", "你好", 0));
                        HSK1.Cards.Add(new Card("Спасибо", "谢谢", 0));
                        HSK1.Cards.Add(new Card("Не, нет", "不", 0));
                        HSK1.Cards.Add(new Card("Быть, является", "是", 0));
                        HSK1.Cards.Add(new Card("Я, меня", "我", 0));
                        HSK1.Cards.Add(new Card("Ты, вы", "你", 0));
                        HSK1.Cards.Add(new Card("Хороший, хорошо", "好", 0));
                        HSK1.Cards.Add(new Card("Частица вопроса", "吗", 0));
                        HSK1.Cards.Add(new Card("Что, какой", "什么", 0));
                        HSK1.Cards.Add(new Card("Человек, люди", "人", 0));
                        cardBunlesCollection.Insert(HSK1);

                        var HSK2 = new CardsBundle("HSK2", 200, 0);
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        HSK2.Cards.Add(new Card("Rus2", "Chn", 1));
                        cardBunlesCollection.Insert(HSK2);

                        var HSK3 = new CardsBundle("HSK3", 300, 0);
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        HSK3.Cards.Add(new Card("Rus3", "Chn", 1));
                        cardBunlesCollection.Insert(HSK3);
                    }
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string cardBundleName = string.Empty;
           if(sender is Button button)
           {
                cardBundleName = button.Tag.ToString();
           }
           CardsMemoryGame cardsMemoryGame = new CardsMemoryGame(cardBundleName);
           cardsMemoryGame.Show();
           this.Close();
        }

        private void ButtonAddNewBundle_Click(object sender, RoutedEventArgs e)
        {
            CreateBundleFirtsStep createBundleFirtsStep = new CreateBundleFirtsStep();
            createBundleFirtsStep.Show();
            this.Close();
        }
        private async void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выберите файл",
                Filter = "Все файлы (*.*)|*.*",
                Multiselect = false
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filePath = dialog.FileName;

                CardsBundle bundle = new CardsBundle();

                string content = await File.ReadAllTextAsync(filePath);

                int startIndexForCards = 0;

                for (int i = 0; i < content.Length; i++)
                {
                    if (content[i] == '\n')
                    {
                        startIndexForCards = i;
                        bundle.Name = content.Substring(0, i + 1);
                        bundle.Name = bundle.Name.Replace("\r\n", "");
                        break;
                    }
                }

                startIndexForCards++;
                for (int i = startIndexForCards; i < content.Length; i++)
                {
                    if (content[i] == '\n')
                    {
                        string temp = content.Substring(startIndexForCards, i - startIndexForCards);
                        temp = temp.Replace("\r", "");
                        for (int j = 0; j < temp.Length; j++)
                        {
                            if (temp[j] == '/')
                            {
                                string chn = temp.Substring(0, j);
                                chn = chn.Replace("\n", "");
                                string rus = temp.Substring(j + 1, temp.Length - j - 1);
                                bundle.Cards.Add(new Card(rus, chn, 0));
                            }
                        }
                        startIndexForCards = i;
                    }
                }
                bundle.NumberOfWords = bundle.Cards.Count;
                bundle.LeftWords = bundle.Cards.Count;

                using(var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
                {
                    var cardBunlesCollection = db.GetCollection<CardsBundle>("CardBundles");
                    cardBunlesCollection.Insert(bundle);
                }

                Items.Add(bundle);
            }
        }
    }
}
