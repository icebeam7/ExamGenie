namespace AISchoolManagementApp.DbModels
{
    public class Exam : BaseTable
    {
        public string Topic { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
