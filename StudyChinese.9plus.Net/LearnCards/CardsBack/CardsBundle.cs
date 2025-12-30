using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChinese._9plus.Net.LearnCards.CardsBack
{
    public class CardsBundle
    {
        public string Name { get; set; }
        public int NumberOfWords { get; set; }
        public int LeftWords { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        public CardsBundle()
        { 
        }

        public CardsBundle(string name, int numberOfWords, int leftWords )
        {
            Name = name;
            NumberOfWords = numberOfWords;
            LeftWords = leftWords;
        }

        public override string ToString()
        {
            return $"{Name}, left{LeftWords} of {NumberOfWords}";
        }
    }
}
