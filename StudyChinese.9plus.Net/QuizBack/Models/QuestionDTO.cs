using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChinese._9plus.Net.QuizBack.Models
{
    public class QuestionDTO : IEquatable<QuestionDTO>
    {
        public string ConnectedTable { get; set; }
        public string Guess { get; set; }
        public string Answer { get; set; }
        public int Column { get; set; }
        public int Row  { get; set; }

        public QuestionDTO(string guess, string answer, int column, int row, string connectedTable)
        {
            Guess = guess;
            Answer = answer;
            Column = column;
            Row = row;
            ConnectedTable=connectedTable;
        }
        public QuestionDTO()
        { 
        }
        public override string ToString()
        {
            return ConnectedTable + "/" + Guess + "/" + Answer + "/" + Column + "/" + Row + "/";
        }

        public bool Equals(QuestionDTO other)
        {
            if (other is null) return false;

            return ConnectedTable == other.ConnectedTable &&
               Guess == other.Guess &&
               Answer == other.Answer &&
               Row == other.Row &&
               Column == other.Column;

        }

        public override bool Equals(object obj) => Equals(obj as QuestionDTO);

        public string ConvertQuestionDTOToString(QuestionDTO questionDTO)
        {
            return questionDTO.ConnectedTable + "/" + questionDTO.Guess + "/" + questionDTO.Answer + "/" + questionDTO.Column + "/" + questionDTO.Row + "/";
        }
        public static QuestionDTO ConvertStringToQuestionDTO(string str)
        {
            var parts = str.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 5)
                throw new ArgumentException("Некорректная строка для парсинга QuestionDTO", nameof(str));

            return new QuestionDTO
            {
                ConnectedTable = parts[0],
                Guess = parts[1],
                Answer = parts[2],
                Column = int.Parse(parts[3]),
                Row = int.Parse(parts[4])
            };
        }
    }
}
