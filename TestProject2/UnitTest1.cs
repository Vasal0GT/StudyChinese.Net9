using LiteDB;
using StudyChinese._9plus.Net.LearnCards;
using System.Windows.Media.Media3D.Converters;
using StudyChinese._9plus.Net.LearnCards.CardsBack;
using Xunit.Abstractions;

namespace TestProject2
{
    public class UnitTest1 : IDisposable
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        /*
        [Fact]
        public void AddExampleCollections_StartingLearnCardMenu_DBWasCreated()
        {
                LearnCardMenu.AddExampleCollections();
                Assert.True(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "StudyCnDB.db"));
        }
        */
        [Fact]
        public void AddExampleCollections_StartingLearnCardMenu_DBContainsProperInfo()
        {
            _output.WriteLine("Test was started");
            var db = new LiteDatabase(AppDomain.CurrentDomain.BaseDirectory + "StudyCnDB.db");
            string results = string.Empty;

            bool firstCheckPoint = db.CollectionExists("CardBundles");
            _output.WriteLine("1: " + firstCheckPoint);

            bool secondCheckPoint = false;
            foreach (var a in db.GetCollection<CardsBundle>("CardBundles").Query().ToList())
            {
                _output.WriteLine($"2: Получен элемент данного вида: {a}");
                //проверяем на случайный элемент
                if (a.Name == "HSK1")
                    secondCheckPoint = true;
            }
            _output.WriteLine($"second result: {secondCheckPoint}");

            bool thirdCheckPoint = false;
            foreach (var a in db.GetCollection<CardsBundle>("CardBundles").Query().ToList())
            {
                _output.WriteLine($"3: Получен элемент данного вида: {a}");
                //проверяем на случайный элемент
                if (a.Name == "HSK2")
                    thirdCheckPoint = true;
            }
            _output.WriteLine($"third result: {thirdCheckPoint}");

            Assert.True(firstCheckPoint && secondCheckPoint && thirdCheckPoint);
        }

        public void Dispose()
        {
            _output.WriteLine("после теста (Dispose)");
        }
    }
}
