using EduLingual.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Entities
{
    [Table("exam")]
    public class Exam : BaseEntity
    {
        [Column("title")]
        [Required]
        public string Title { get; set; } = string.Empty;
        [Column("center_id")]
        [ForeignKey("Center")]
        public Guid CenterId { get; set; }
        public virtual User Center { get; set; } = null!;
        [Column("total_question")]
        public int TotalQuestion { get; set; } = 0;

        [Column("course_id")]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        [InverseProperty(nameof(Exam))]
        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
    }
}
