using EduLingual.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Feedback;

public class FeedbackViewModel : FeedbackDto
{
    public UserDto? User { get; set; }
}
