using AISchoolManagementApp.DbModels;

namespace AISchoolManagementApp.Services
{
    public interface IDatabaseService
    {
        Task<List<T>> GetItemsAsync<T>() where T : BaseTable, new();
        Task<T> GetItemAsync<T>(int id) where T : BaseTable, new();
        Task AddItemAsync<T>(T item) where T : BaseTable, new();
        Task AddItemsAsync<T>(IEnumerable<T> items) where T : BaseTable, new();
        Task DeleteItemAsync<T>(T item) where T : BaseTable, new();
        Task DeleteItemsAsync<T>(IEnumerable<T> items) where T : BaseTable, new();
    }
}
