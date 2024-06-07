using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseLanguage
{
    public class CourseLanguageDto : BaseEntity
    {
        public string? Name { get; set; }
        public CourseLanguageStatus Status { get; set; }
    }
}
