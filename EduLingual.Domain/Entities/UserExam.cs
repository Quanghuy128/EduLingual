using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Entities
{
    [Table("user_exam")]
    public class UserExam
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Column("exam_id")]
        public Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; } = null!;

        public double Score { get; set; } = 0;
    }
}
