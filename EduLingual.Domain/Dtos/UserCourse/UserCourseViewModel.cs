using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.UserCourse
{
    public class UserCourseViewModel
    {
        public CourseDto Course { get; set; }
        public UserDto User { get; set; }
    }
}
