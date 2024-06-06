using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Dtos.CourseCategory;
using EduLingual.Domain.Dtos.User;

namespace EduLingual.Domain.Dtos.Course
{
    public class CourseViewModel : CourseDto
    {
        public CourseViewModel(string? title, string? description, string? duration, double tuitionfee) : base(title, description, duration, tuitionfee)
        {
        }

        public CourseAreaDto? CourseArea { get; set; }
        public CourseLanguageDto? CourseLanguage { get; set; }
        public CourseCategoryDto? CourseCategory { get; set; }
        public UserDto? Center { get; set; }
    }
}
