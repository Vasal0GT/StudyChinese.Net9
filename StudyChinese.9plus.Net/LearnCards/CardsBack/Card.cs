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
        public int SolveStatus { get; set; }  = 0;
        //0, 1, 2, 3  
        public DateTime TimeExpired { get; set; } = new DateTime(2050, 1, 1, 0, 0, 0, DateTimeKind.Local);
        public Card() { }

        public Card(string rus, string chn, int status)
        {
            RusTranslation = rus;
            ChnTranslation = chn;
            SolveStatus = status;
        }
        public Card(string rus, string chn, int status, DateTime time)
        {
            RusTranslation = rus;
            ChnTranslation = chn;
            SolveStatus = status;
            TimeExpired = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Local);
        }
    }
}
