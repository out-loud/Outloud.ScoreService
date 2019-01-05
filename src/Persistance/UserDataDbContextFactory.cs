using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Outloud.ScoreService.Persistance
{
    public class UserDataDbContextFactory : IDesignTimeDbContextFactory<UserDataDbContext>
    {
        public UserDataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDataDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1434;Database=master;User=sa;Password=1qazxsW@;");

            return new UserDataDbContext(optionsBuilder.Options);
        }
    }
}
