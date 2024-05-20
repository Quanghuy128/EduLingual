using EduLingual.Domain.Entities;
using EduLingual.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Course
{
    public class CreateCourseRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [StringLength(1000)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Duration { get; set; } = string.Empty;
        [Required]
        public string CourseAreaId { get; set; }
        [Required]
        public string CourseLanguageId { get; set; }
        [Required]
        public string CourseCategoryId { get; set; }
        [Required]
        public string CenterId { get; set; }
    }
}
