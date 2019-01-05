using Microsoft.EntityFrameworkCore;
using Outloud.ScoreService.Persistance.Models;

namespace Outloud.ScoreService.Persistance
{
    public class UserDataDbContext : DbContext
    {
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<UserDataEntity> UserData { get; set; }

        public UserDataDbContext(DbContextOptions<UserDataDbContext> options) : base(options)
        {
        }
    }
}
