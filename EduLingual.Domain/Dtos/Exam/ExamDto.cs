using EduLingual.Domain.Dtos.User;

namespace EduLingual.Domain.Dtos.Exam
{
    public class ExamDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Title { get; set; }
        public int TotalQuestion { get; set; }
        public virtual UserDto Center { get; set; } = null!;

        public ExamDto()
        {
        }

        public ExamDto(Guid id, DateTime createdAt, bool isDeleted, string title, int totalQuestion, UserDto center)
        {
            Id = id;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
            Title = title;
            TotalQuestion = totalQuestion;
            Center = center;
        }
    }
}
