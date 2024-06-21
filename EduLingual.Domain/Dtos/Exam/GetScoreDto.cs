using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Exam;

public class GetScoreDto
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
}
