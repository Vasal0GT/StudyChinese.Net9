using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChinese._9plus.Net.QuizBack.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
        public int Multiplier { get; set; }
        public Dictionary<int, string> RowThemes { get; set; } = new Dictionary<int, string>();

        public Table(Guid id, string name, string description, int rowNumber, int columnNumber, List<QuestionDTO> questions, int multiplier)
        {
            Id = id;
            Name = name;
            Description = description;
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            Questions = questions ?? new List<QuestionDTO>();
            Multiplier=multiplier;
            RowThemes = new Dictionary<int, string>();
        }
        public Table()
        { 
        }

        //нужно добавить два метода по сериализации всех полученных значений из экзмепляра лкасса в json 
        public static string ConvertToJson(Table table, string tableName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(table);
                string baseDerictory = AppDomain.CurrentDomain.BaseDirectory;
                string projcetDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string folderPath = Path.Combine(projcetDirectory, @"QuizBack\JsonTables");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, $"{tableName}.json");

                File.WriteAllText(filePath, json);
                return "Викторина была успешно сохранена";
            } catch
            {
                return "Ошибка в создании викторины";
            }
        }
    }
}
