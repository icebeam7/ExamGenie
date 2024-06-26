namespace AISchoolManagementApp.DbModels
{
    public class Question : BaseTable
    {
        public int Number { get; set; }
        public string Content { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        public string Reference { get; set; }
        public Citation Citation { get; set; }
        public int ExamId { get; set; }
        public int CitationId { get; set; }
    }
}
