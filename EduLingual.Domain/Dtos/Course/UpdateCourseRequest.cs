using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Course
{
    public class UpdateCourseRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        [Range(100000, double.MaxValue, ErrorMessage = MessageConstant.Vi.Course.Require.ValidFee)]
        public double? Tuitionfee { get; set; }
        public Guid? CenterId { get; set; }
        public Guid? CourseAreaId { get; set; }
        public Guid? CourseLanguageId { get; set; }
        public Guid? CourseCategoryId { get; set; }
        public CourseStatus? Status { get; set; }
    }
}
