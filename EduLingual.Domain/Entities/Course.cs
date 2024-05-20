using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using EduLingual.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Entities
{
    [Table("course")]
    public class Course : BaseEntity
    {
        [Column("title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column("duration")]
        public string Duration {  get; set; } = string.Empty;

        [Column("tuition_fee")]
        public double Tuitionfee { get; set; } = 0;

        [Column("status")]
        public CourseStatus Status { get; set; } = CourseStatus.Active;

        [Column("area_id")]
        [ForeignKey(nameof(CourseArea))]
        public Guid CourseAreaId { get; set; }
        public virtual CourseArea CourseArea { get; set; } = null!;

        [Column("language_id")]
        [ForeignKey(nameof(CourseLanguage))]
        public Guid CourseLanguageId { get; set; }
        public virtual CourseLanguage CourseLanguage { get; set; } = null!;

        [Column("category_id")]
        [ForeignKey(nameof(CourseCategory))]
        public Guid CourseCategoryId { get; set; }
        public virtual CourseCategory CourseCategory { get; set; } = null!;

        [Column("center_id")]
        [ForeignKey("Center")]
        public Guid CenterId { get; set; }
        public virtual User Center { get; set; } = null!;

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; } = new List<CourseFeedback>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    }
}
