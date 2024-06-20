using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduLingual.Api.Controllers
{
    public class CourseController : BaseController<CourseController>
    {
        private readonly ICourseService _courseService;

        public CourseController(ILogger<CourseController> logger, ICourseService courseService) : base(logger)
        {
            _courseService = courseService;
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesPaginationEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] string? title, [FromQuery] CourseStatus? status, [FromQuery] Guid? centerId, [FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<CourseViewModel> result = await _courseService.GetPagination(page, size, title, status, centerId);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.Course.CoursesEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses([FromQuery] string? title, [FromQuery] CourseFilter? courseFilter, string? sort, [FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<CourseViewModel> result = await _courseService.GetCourses(page, size, title, courseFilter, sort);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.Course.CourseEndpoint)]
        [ProducesResponseType(typeof(Result<CourseViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            Result<CourseViewModel> course = await _courseService.GetCourseById(id);
            return Ok(course);
        }

        [HttpPost(ApiEndPointConstant.Course.CoursesEndpoint)]
        [ProducesResponseType(typeof(Result<CourseViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
        {
            Result<CourseViewModel> result = await _courseService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut(ApiEndPointConstant.Course.CourseEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
        {
            Result<bool> result = await _courseService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.Course.CourseEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _courseService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.Course.HighlightedCoursesEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHighlightedCourses()
        {
            Result<List<CourseViewModel>> courses = await _courseService.GetHighlightedCourses();
            return Ok(courses);
        }
    }
}
