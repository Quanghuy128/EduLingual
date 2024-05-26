using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Feedback;

public class FeedbackDto : BaseEntity
{
    public string Description { get; set; }
    public int? Rating { get; set; }
    public FeedbackStatus FeedbackStatus { get; set; }
}
