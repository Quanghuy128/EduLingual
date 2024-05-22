using EduLingual.Domain.Entities;
using EduLingual.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.Course
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public double Tuitionfee { get; set; }
        public Entities.CourseArea? CourseArea { get; set; }
        public Entities.CourseLanguage? CourseLanguage { get; set; } 
        public Entities.CourseCategory? CourseCategory { get; set; }
        public Entities.User? Center { get; set; }
    }
}
