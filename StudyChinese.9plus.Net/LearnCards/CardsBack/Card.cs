using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChinese._9plus.Net.LearnCards.CardsBack
{
    public class Card
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RusTranslation { get; set; }
        public string ChnTranslation { get; set; }
        public int SolveStatus { get; set; }
        // 1, 2, 3  

        public Card() { }

        public Card(string rus, string chn, int status)
        {
            RusTranslation = rus;
            ChnTranslation = chn;
            if (SolveStatus!< 1 || SolveStatus!> 3)
            {
                SolveStatus = status;
            }
            else
                throw new ArgumentException("Card's status can't be set");
        }

    }
}
