using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduLingual.Domain.Common;

namespace EduLingual.Domain.Entities
{
    [Table("question")]
    public class Question : BaseEntity
    {
        [Column("content")]
        [Required]
        public string Content { get; set; } = string.Empty;
        [Column("point")]
        [Required]
        public double Point { get; set; } = 0;

        [Column("exam_id")]
        [ForeignKey(nameof(Exam))]
        public Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; } = null!;

        [InverseProperty(nameof(Question))]
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
