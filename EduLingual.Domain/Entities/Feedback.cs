using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Entities
{
    [Table("feedback")]
    public class Feedback : BaseEntity
    {
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("rating")]
        public int? Rating { get; set; }

        [Column("status")]
        [EnumDataType(typeof(FeedbackStatus))]
        public FeedbackStatus FeedbackStatus { get; set; } = FeedbackStatus.Active;

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; } = new List<CourseFeedback>();

    }
}
