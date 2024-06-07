using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseCategory
{
    public class UpdateCourseCategoryRequest
    {
        public string? Name { get; set; }
        public Guid? LanguageId { get; set; }
        public CourseCategoryStatus? Status { get; set; }
    }
}
