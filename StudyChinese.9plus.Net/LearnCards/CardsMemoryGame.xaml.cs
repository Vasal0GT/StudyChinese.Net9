using LiteDB;
using Microsoft.Extensions.AI;
using StudyChinese._9plus.Net.LearnCards.CardsBack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for CardsMemoryGame.xaml
    /// </summary>
    public partial class CardsMemoryGame : Window, INotifyPropertyChanged
    {
        static readonly string localDir = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _nameOfBundle;
        public event PropertyChangedEventHandler? PropertyChanged;

        private Card _choosenCard;
        public Card choosenCard
            {
            get => _choosenCard;
            set
            {
                _choosenCard = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(choosenCard)));
            }
        }

        public CardsMemoryGame(string nameOfBundle)
        {
            _nameOfBundle = nameOfBundle;
            InitializeComponent();
            DataContext = this;
            Loaded += ShowStatusInfo;
        }
        private void ShowStatusInfo(object sender, RoutedEventArgs e)
        {
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                var cardsBundleCollection = db.GetCollection<CardsBundle>("CardBundles");

                var temp = cardsBundleCollection.Query()
                .Where(x => x.Name == _nameOfBundle)
                .ToList();

                var allNotSolvedCards = temp[0].Cards.Where(card => card.SolveStatus == 0).ToList();
                var allCardsStillSolving = temp[0].Cards.Where(card => card.SolveStatus == 1 || card.SolveStatus == 2).ToList();
                var allSolvedCards = temp[0].Cards.Where(card => card.SolveStatus == 3).ToList();

                //тут типо надо сдеелать так чтобы в ChoosenCard, попала соответсвенно выбранная карта
                choosenCard = ChooseNextCard(temp[0].Cards);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    NotStartedNumberOfCards.Text = allNotSolvedCards.Count.ToString();
                    StillLaerningNumberOfCards.Text = allCardsStillSolving.Count.ToString();
                    LearnedNumberOfCards.Text = allSolvedCards.Count.ToString();
                });
            }
        }
        private static Card ChooseNextCard(List<Card> cards)
        {
            //Сначала надо проверить карточки у которых время закончилось
            DateTime timeNow = DateTime.Now;

            var expiredCards = cards.Where(card => card.TimeExpired < timeNow);
            if (expiredCards.Count() > 0)
            { 
                return expiredCards.First();
            }

            //А тут типо если  нет просроченных, просто пикаем по статусу
            cards.Sort((a, b) => a.SolveStatus.CompareTo(b.SolveStatus));
            //Если их много то пусть просто рандомный аикается, это самый простой вариант
            List<Card> targetedCards = cards.Where(x => x.SolveStatus == cards.First().SolveStatus).ToList();
            if (cards.Count > 1)
            {
                Random random = new Random();
                var randomNumber = random.Next(targetedCards.Count);
                return targetedCards[randomNumber];
            }

            return cards.First();
        }
        private void ShowAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAnswerButton.Visibility = Visibility.Collapsed;
            AnswerButtonsPanel.Visibility = Visibility.Visible;
            CheneseWord.Visibility = Visibility.Visible;
        }

        private void OneDayPlus_Button(object sender, RoutedEventArgs e)
        {
            DateTime newTimeForCard = DateTime.Now.AddDays(1);
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                var cardsBundleCollection = db.GetCollection<CardsBundle>("CardBundles");
                var targetBundle = cardsBundleCollection.FindOne(x => x.Name == _nameOfBundle);
                if (targetBundle != null)
                {
                    Guid cardIdToUpdate = choosenCard.Id;
                    var targetCard = targetBundle.Cards.FirstOrDefault(c => c.Id == cardIdToUpdate);
                    if (targetCard != null)
                    {
                        targetCard.SolveStatus = 3;
                        targetCard.TimeExpired = newTimeForCard;
                        cardsBundleCollection.Update(targetBundle);
                    }
                }
            }
            CardsMemoryGame cardGame = new CardsMemoryGame(_nameOfBundle);
            cardGame.Show();
            this.Close();
        }

        private void SevenMimutesPlus_Button(object sender, RoutedEventArgs e)
        {
            DateTime newTimeForCard = DateTime.Now.AddMinutes(7);
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                var cardsBundleCollection = db.GetCollection<CardsBundle>("CardBundles");
                var targetBundle = cardsBundleCollection.FindOne(x => x.Name == _nameOfBundle);
                if (targetBundle != null)
                {
                    Guid cardIdToUpdate = choosenCard.Id;
                    var targetCard = targetBundle.Cards.FirstOrDefault(c => c.Id == cardIdToUpdate);
                    if (targetCard != null)
                    {
                        targetCard.SolveStatus = 2;
                        targetCard.TimeExpired = newTimeForCard;
                        cardsBundleCollection.Update(targetBundle);
                    }
                }
            }
            CardsMemoryGame cardGame = new CardsMemoryGame(_nameOfBundle);
            cardGame.Show();
            this.Close();
        }

        private void TwoMinutesPlus_Button(object sender, RoutedEventArgs e)
        {
            DateTime newTimeForCard = DateTime.Now.AddMinutes(2);
            using (var db = new LiteDatabase(localDir +  "StudyCnDB.db"))
            {
                var cardsBundleCollection = db.GetCollection<CardsBundle>("CardBundles");
                var targetBundle = cardsBundleCollection.FindOne(x => x.Name == _nameOfBundle);
                if (targetBundle != null)
                {
                    Guid cardIdToUpdate = choosenCard.Id;
                    var targetCard = targetBundle.Cards.FirstOrDefault(c => c.Id == cardIdToUpdate);
                    if (targetCard != null)
                    {
                        targetCard.SolveStatus = 1;
                        targetCard.TimeExpired = newTimeForCard;
                        cardsBundleCollection.Update(targetBundle);
                    }
                }
            }
            CardsMemoryGame cardGame = new CardsMemoryGame(_nameOfBundle);
            cardGame.Show();
            this.Close();
        }
    }
}
