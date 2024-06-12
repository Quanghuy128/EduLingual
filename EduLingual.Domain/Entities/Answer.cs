using EduLingual.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Entities
{
    public class Answer : BaseEntity
    {
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("is_correct")]
        public bool IsCorrect { get; set; } = false;

        [Column("question_id")]
        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}
