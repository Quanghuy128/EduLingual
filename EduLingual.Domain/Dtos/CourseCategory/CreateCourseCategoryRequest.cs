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
    public class CreateCourseCategoryRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.CourseCategory.Require.NameRequired)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = MessageConstant.Vi.CourseCategory.Require.LanguageRequired)]
        public Guid LanguageId {  get; set; }
        public CourseCategoryStatus CourseCategoryStatus { get; set; } = CourseCategoryStatus.Available;
    }
}
