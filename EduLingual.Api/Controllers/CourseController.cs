﻿using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    public class CourseController : BaseController<CourseController>
    {
        private readonly ICourseService _courseService;

        public CourseController(ILogger<CourseController> logger, ICourseService courseService) : base(logger)
        {
            _courseService = courseService;
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesEndpoint)]
        public async Task<IActionResult> GetCoursesByArea([FromQuery] CourseFilter courseFilter)
        {
            List<CourseViewModel> courses = await _courseService.GetCourses(courseFilter);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByAreaEndpoint)]
        public async Task<IActionResult> GetCoursesByArea([FromQuery] string areaName)
        {
            List<CourseViewModel> courses = await _courseService.GetCoursesByArea(areaName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByLanguageEndpoint)]
        public async Task<IActionResult> GetCoursesByLanguage([FromQuery] string languageName)
        {
            List<CourseViewModel> courses = await _courseService.GetCoursesByLanguage(languageName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByCategoryEndpoint)]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery] string categoryName)
        {
            List<CourseViewModel> courses = await _courseService.GetCoursesByCategory(categoryName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CourseEndpoint)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            CourseViewModel courses = await _courseService.GetCourseById(id);
            return Ok(courses);
        }
    }
}
