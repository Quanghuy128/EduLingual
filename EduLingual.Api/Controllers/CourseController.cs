using EduLingual.Application.Service;
using EduLingual.Domain.Common;
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
            Result<List<CourseViewModel>> courses = await _courseService.GetCourses(courseFilter);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByAreaEndpoint)]
        public async Task<IActionResult> GetCoursesByArea([FromQuery] string areaName)
        {
            Result<List<CourseViewModel>> courses = await _courseService.GetCoursesByArea(areaName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByLanguageEndpoint)]
        public async Task<IActionResult> GetCoursesByLanguage([FromQuery] string languageName)
        {
            Result<List<CourseViewModel>> courses = await _courseService.GetCoursesByLanguage(languageName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByCategoryEndpoint)]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery] string categoryName)
        {
            Result<List<CourseViewModel>> courses = await _courseService.GetCoursesByCategory(categoryName);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CourseEndpoint)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            Result<CourseViewModel> course = await _courseService.GetCourseById(id);
            return Ok(course);
        }
    }
}
