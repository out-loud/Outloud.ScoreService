using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outloud.ScoreService.Persistance.Models
{
    [Table("Courses")]
    public class CourseEntity
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public int Progress { get; set; }
        public string DisplayName { get; set; }
    }
}