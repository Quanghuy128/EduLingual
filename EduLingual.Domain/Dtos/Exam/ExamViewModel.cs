using EduLingual.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Exam;

public class ExamViewModel : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public Guid CenterId { get; set; }
    public Guid CourseId { get; set; }
}
