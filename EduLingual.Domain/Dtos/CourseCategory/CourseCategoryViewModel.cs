using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseCategory
{
    public class CourseCategoryViewModel : CourseCategoryDto
    {
        public CourseLanguageDto? CourseLanguage { get; set; }
    }
}
