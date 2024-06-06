using EduLingual.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Course
{
    public class CourseDto : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public double TuitionFee { get; set; }
        public CourseDto(string? title, string? description, string? duration, double tuitionFee)
        {
            Title = title;
            Description = description;
            Duration = duration;
            TuitionFee = tuitionFee;
        }
    }
}
