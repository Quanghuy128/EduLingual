using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Feedback;

public class CreateFeedbackRequest
{
    [Required(ErrorMessage = MessageConstant.Vi.Feedback.Require.DescriptionRequired)]
    public string Description { get; set; }
    public int? Rating { get; set; }
    [Required(ErrorMessage = MessageConstant.Vi.Feedback.Require.UserRequired)]
    public Guid UserId { get; set; }
}
