using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Course
{
    public class CourseByUserDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public double Tuitionfee { get; set; }
        public string CenterName { get; set; }

        public CourseByUserDto(Guid id, string? title, string? description, string? duration, double tuitionFee, string centerName)
        {
            Id = id;
            Title = title;
            Description = description;
            Duration = duration;
            Tuitionfee = tuitionFee;
            CenterName = centerName;
        }

        public CourseByUserDto()
        {
        }
    }
}
