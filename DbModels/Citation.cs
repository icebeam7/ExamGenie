namespace AISchoolManagementApp.DbModels
{
    public class Citation : BaseTable
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Url { get; set; }
    }
}
