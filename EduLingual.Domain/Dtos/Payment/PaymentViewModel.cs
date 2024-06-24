using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;

namespace EduLingual.Domain.Dtos.Payment
{
    public class PaymentViewModel : PaymentDto
    {

        public CourseViewModel Course { get; set; }
        public UserDto User { get; set; }
    }
}
