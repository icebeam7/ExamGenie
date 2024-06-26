using Microsoft.EntityFrameworkCore;
using AISchoolManagementApp.DbModels;

namespace AISchoolManagementApp.DbContexts
{
    public class SmartSchoolContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Citation> Citations { get; set; }

        public SmartSchoolContext(DbContextOptions options)
            : base(options)
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
    }
}
