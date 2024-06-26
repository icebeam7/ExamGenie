namespace AISchoolManagementApp.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Citation> Citations { get; set; }
    }
}
