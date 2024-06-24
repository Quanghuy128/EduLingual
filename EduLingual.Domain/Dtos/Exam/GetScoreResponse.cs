using EduLingual.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int TotalQuestionRight { get; set; }
        public int TotalQuestionWrong { get; set; }
        public virtual ExamDto Exam { get; set; } = null!;
        public virtual UserDto Center { get; set; } = null!;

        public GetScoreResponse(Guid id, DateTime createdAt, double score, int totalQuestionRight, int totalQuestionWrong, ExamDto exam, UserDto center)
        {
            Id = id;
            CreatedAt = createdAt;
            Score = score;
            TotalQuestionRight = totalQuestionRight;
            TotalQuestionWrong = totalQuestionWrong;
            Exam = exam;
            Center = center;
        }

        public GetScoreResponse()
        {
        }
    }
}
