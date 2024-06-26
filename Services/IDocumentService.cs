namespace AISchoolManagementApp.Services
{
    public interface IDocumentService
    {
        Task<bool> UploadDocumentAsync(FileResult file);
        IAsyncEnumerable<string> ListAllDocumentsAsync(int pageSize = 50);
    }
}
