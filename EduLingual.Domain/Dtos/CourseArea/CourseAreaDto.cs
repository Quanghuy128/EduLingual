using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduLingual.Domain.Common;

namespace EduLingual.Domain.Dtos.CourseArea
{
    public class CourseAreaDto : BaseEntity
    {
        public string? Name { get; set; }
        public CourseAreaStatus Status { get; set; }

    }
}
