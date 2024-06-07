using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseCategory
{
    public class CourseCategoryDto : BaseEntity
    {
        public string? Name { get; set; }
        public CourseCategoryStatus Status { get; set; }
    }
}
