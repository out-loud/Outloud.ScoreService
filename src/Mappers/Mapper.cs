using Outloud.ScoreService.DTO;
using Outloud.ScoreService.Persistance.Models;

namespace Outloud.ScoreService.Mappers
{
    public static class Mapper
    {
        public static CourseEntity Map(UserDataDTO dataDTO) => new CourseEntity
        {
            DisplayName = dataDTO.CourseName,
            Progress = dataDTO.Progress,
            CourseId = dataDTO.CourseId
        };
    }
}
