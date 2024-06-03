using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Dtos.CourseLanguage
{
    public class CreateCourseLanguageRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.CourseLanguage.Require.NameRequired)]
        public string Name { get; set; } = string.Empty;
        public CourseLanguageStatus Status { get; set; } = CourseLanguageStatus.Available;
    }
}
