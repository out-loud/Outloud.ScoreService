using System;

namespace Outloud.ScoreService.DTO
{
    public class UserDataDTO
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int Progress { get; set; }
    }
}
