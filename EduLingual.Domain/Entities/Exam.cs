using EduLingual.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Entities
{
    public class Exam : BaseEntity
    {
        [Column("creator_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;


        [Column("exam_id")]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        [InverseProperty(nameof(Exam))]
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
