using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Infrastructure
{
    [Table("user_course")]
    public class UserCourse
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Column("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        [Column("joined_at")]
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        [Column("finished_at")]
        public DateTime FinishedAt { get; set; }

        [Column("status")]
        [EnumDataType(typeof(UserStatus))]
        public UserCourseStatus Status { get; set; } = UserCourseStatus.Studying;
    }
}
