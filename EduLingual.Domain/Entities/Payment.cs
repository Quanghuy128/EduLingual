using EduLingual.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Entities
{
    [Table("payment")]
    public class Payment : BaseEntity
    {
        [Column("full_name")]
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Column("phone_number")]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("method")]
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        [Column("fee")]
        public double Fee { get; set; } = 0;

        [Column("course_id")]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
