using EduLingual.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Infrastructure
{
    [Table("user_course")]
    public class UserCourse
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Column("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
