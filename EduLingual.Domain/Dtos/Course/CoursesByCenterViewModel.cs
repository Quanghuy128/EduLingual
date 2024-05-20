using EduLingual.Domain.Entities;

namespace EduLingual.Domain.Dtos.Course
{
    public class CoursesByCenterViewModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
