﻿using EduLingual.Domain.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.User
{
    public class UserCourseDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Course { get; set; }

        public UserCourseDto(string userName, string fullName, string description, DateTime joinedAt, string course)
        {
            UserName = userName;
            FullName = fullName;
            Description = description;
            JoinedAt = joinedAt;
            Course = course;
        }

        public UserCourseDto()
        {
        }
    }
}
