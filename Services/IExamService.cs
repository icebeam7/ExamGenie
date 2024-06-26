using AISchoolManagementApp.Models;

namespace AISchoolManagementApp.Services
{
    public interface IExamService
    {
        Task<Exam> GenerateNewExamAsync(string message);
        Task<List<Exam>> GetExamsAsync();
        Task<Exam> GetExamWithDetailsAsync(int id);
        IEnumerable<Citation> FindCitation(string reference,
            List<Citation> citations);
        Task SaveExamAsync(Exam exam);
        Task DeleteExamAsync(Exam exam);
        Task ViewPDFAsync(Exam exam);
    }
}
