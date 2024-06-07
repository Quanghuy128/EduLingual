using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseArea
{
    public class CreateCourseAreaRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.CourseArea.Require.NameRequired)]
        public string Name { get; set; } = string.Empty;
        public CourseAreaStatus Status { get; set; } = CourseAreaStatus.Available;
    }
}
