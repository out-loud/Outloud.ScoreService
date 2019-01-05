using System.Threading.Tasks;

namespace Outloud.ScoreService.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDataDbContext context;

        public UnitOfWork(UserDataDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
