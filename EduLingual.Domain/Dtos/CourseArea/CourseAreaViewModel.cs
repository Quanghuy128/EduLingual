﻿using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.CourseArea
{
    public class CourseAreaViewModel
    {
        public string? Name { get; set; }
        public CourseAreaStatus CourseAreaStatus { get; set; }
    }
}
