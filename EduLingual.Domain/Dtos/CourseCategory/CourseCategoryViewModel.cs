using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseCategory
{
    public class CourseCategoryViewModel
    {
        public string? Name { get; set; }
        public Guid? LanguageId { get; set; }
        public CourseCategoryStatus CourseCategoryStatus { get; set; }
    }
}
