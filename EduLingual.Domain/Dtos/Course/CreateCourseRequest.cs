using System.ComponentModel.DataAnnotations;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.Course
{
    public class CreateCourseRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.TitleRequired)]
        public string Title { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.DescriptionRequired)]
        public string Description { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.DurationRequired)]
        public string Duration { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.TutionFeeRequired)]
        public double Tuitionfee { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.CenterRequired)]
        public Guid CenterId { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.AreaRequired)]
        public Guid CourseAreaId { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.LanguageRequired)]
        public Guid CourseLanguageId { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Course.Require.CategoryRequired)]
        public Guid CourseCategoryId { get; set; }
        public CourseStatus CourseStatus { get; set; } = CourseStatus.Active;
    }
}
