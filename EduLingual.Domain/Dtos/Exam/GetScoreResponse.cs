using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Exam
{
    public class GetScoreResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Score { get; set; }
        public virtual ExamDto Exam { get; set; } = null!;

        public GetScoreResponse(Guid id, DateTime createdAt, double score, ExamDto exam)
        {
            Id = id;
            CreatedAt = createdAt;
            Score = score;
            Exam = exam;
        }

        public GetScoreResponse()
        {
        }
    }
}
