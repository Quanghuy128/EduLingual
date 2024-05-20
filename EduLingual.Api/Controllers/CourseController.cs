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
        public async Task<IActionResult> GetCourses([FromQuery] CourseFilter courseFilter)
        {
            Result<List<CourseViewModel>> courses = await _courseService.GetCourses(courseFilter);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesByCenterIdEndpoint)]
        public async Task<IActionResult> GetCoursesByCenterId(Guid id)
        {
            Result<CoursesByCenterViewModel> courses = await _courseService.GetCoursesByCenterId(id);
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
