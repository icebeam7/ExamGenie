using Microsoft.EntityFrameworkCore;
using AISchoolManagementApp.DbContexts;
using AISchoolManagementApp.DbModels;

using System.Linq;

namespace AISchoolManagementApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        SmartSchoolContext dbContext;

        public DatabaseService(SmartSchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : BaseTable, new()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetItemAsync<T>(int id) where T : BaseTable, new()
        {
            return await dbContext.FindAsync<T>(id);
        }

        public async Task AddItemAsync<T>(T item) where T : BaseTable, new()
        {
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddItemsAsync<T>(IEnumerable<T> items) where T : BaseTable, new()
        {
            await dbContext.AddRangeAsync(items);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync<T>(T item) where T : BaseTable, new()
        {
            dbContext.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemsAsync<T>(IEnumerable<T> items) where T : BaseTable, new()
        {
            dbContext.RemoveRange(items);
            await dbContext.SaveChangesAsync();
        }
    }
}
