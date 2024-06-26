namespace AISchoolManagementApp.Models
{
    public class References
    {
        public IEnumerable<Citation> citations { get; set; }
    }

    public class Citation
    {
        public string title { get; set; }
        public string filepath { get; set; }
        public string url { get; set; }
    }
}
