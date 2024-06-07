using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Dtos.CourseCategory
{
    public class CreateCourseCategoryRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.CourseCategory.Require.NameRequired)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = MessageConstant.Vi.CourseCategory.Require.LanguageRequired)]
        public Guid LanguageId {  get; set; }
        public CourseCategoryStatus Status { get; set; } = CourseCategoryStatus.Available;
    }
}
