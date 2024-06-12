using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Exam;

public class ResultExamDto
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public List<Guid> Results { get; set; }
}
