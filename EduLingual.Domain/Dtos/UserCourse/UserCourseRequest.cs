using EduLingual.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.UserCourse;

public class UserCourseRequest
{
    [Required(ErrorMessage = MessageConstant.Vi.UserCourse.Require.UserIdRequired)]
    public Guid UserId { get; set; } = Guid.Empty;
    [Required(ErrorMessage = MessageConstant.Vi.UserCourse.Require.CourseIdRequired)]
    public Guid CourseId { get; set; } = Guid.Empty;
}
