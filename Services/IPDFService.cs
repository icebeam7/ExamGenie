using AISchoolManagementApp.Models;

namespace AISchoolManagementApp.Services
{
    public interface IPDFService
    {
        Task GeneratePDFAsync(Exam exam);
    }
}
