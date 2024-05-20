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
    public class UpdateCourseAreaRequest
    {
        public string? Name { get; set; }
        public CourseAreaStatus? CourseAreaStatus { get; set; }
    }
}
