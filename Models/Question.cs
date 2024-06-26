using System.Text.Json.Serialization;

namespace AISchoolManagementApp.Models
{
    public class Questions
    {
        public IEnumerable<Question> questions { get; set; }
    }

    public class Question
    {
        public int Number { get; set; }
        public string Content { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        public string Reference { get; set; }

        [JsonIgnore]
        public Citation Citation { get; set; }
        public string SelectedOption { get; set; }
    }
}
