using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Entities
{
    public class CourseArea
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("status")]
        [EnumDataType(typeof(CourseAreaStatus))]
        public CourseAreaStatus Status { get; set; } = CourseAreaStatus.Available;

        [InverseProperty(nameof(CourseArea))]
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
