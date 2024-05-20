using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Entities
{
    [Table("course_category")]
    public class CourseCategory
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("status")]
        [EnumDataType(typeof(CourseCategoryStatus))]
        public CourseCategoryStatus Status { get; set; } = CourseCategoryStatus.Available;

        [Column("language_id")]
        [ForeignKey(nameof(CourseLanguage))]
        public Guid LanguageId { get; set; }
        public CourseLanguage CourseLanguage { get; set; } = null!;

        [InverseProperty(nameof(CourseCategory))]
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
