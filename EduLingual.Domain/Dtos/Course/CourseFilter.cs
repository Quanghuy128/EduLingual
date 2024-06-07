using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Course
{
    public class CourseFilter
    {
        public Guid? AreaId { get; set; }
        public Guid? LanguageId { get; set; } 
        public Guid? CategoryId { get; set; }
    }
}
