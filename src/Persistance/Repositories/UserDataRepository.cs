using Microsoft.EntityFrameworkCore;
using Outloud.ScoreService.Persistance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outloud.ScoreService.Persistance.Repositories
{
    public interface IUserDataRepository
    {
        Task AddOrUpdateProgressAsync(string userId, CourseEntity courseEntity);
        Task<UserDataEntity> GetUserData(string userId);
    }

    public class UserDataRepository : IUserDataRepository
    {
        private readonly UserDataDbContext _context;

        public UserDataRepository(UserDataDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateProgressAsync(string userId, CourseEntity courseEntity)
        {
            var entity = await _context.UserData.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == userId);

            if (entity == null)
            {
                var user = new UserDataEntity(userId);
                user.AddCourse(courseEntity);
                _context.Add(user);
            }
            else
            {
                var course = entity.Courses.FirstOrDefault(x => x.CourseId == courseEntity.CourseId);
                if (course == null)
                    entity.Courses.Add(courseEntity);
                else
                    course.Progress = courseEntity.Progress;
            }
        }

        public async Task<UserDataEntity> GetUserData(string userId)
        {
            return await _context.UserData.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
