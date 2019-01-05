using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outloud.ScoreService.Persistance.Models
{
    [Table("UserData")]
    public class UserDataEntity
    {
        public string Id { get; set; }
        public ICollection<CourseEntity> Courses{ get; set; }

        public UserDataEntity(string id)
        {
            Id = id;
            Courses = new List<CourseEntity>();
        }

        internal void AddCourse(CourseEntity courseEntity)
        {
            Courses.Add(courseEntity);
        }
    }
}
