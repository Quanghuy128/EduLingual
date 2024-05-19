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
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public Guid CourseAreaId { get; set; }
        public Guid CourseLanguageId { get; set; }
        public Guid CourseCategoryId { get; set; }

        /*public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<CourseFeedback> CourseFeedbacks { get; set; } = new List<CourseFeedback>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();*/

    }
}
