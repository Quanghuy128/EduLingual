using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Entities
{
    public class CourseFeedback
    {
        [Column("course_id")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        [Column("feedback_id")]
        public Guid FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; } = null!;
    }
}
