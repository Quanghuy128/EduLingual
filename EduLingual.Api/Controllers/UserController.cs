using EduLingual.Api.Configuration;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Enum;
using EduLingual.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpGet(ApiEndPointConstant.User.UsersEndpoint)]
        [ProducesResponseType(typeof(Result<List<UserViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<UserViewModel> result = await _userService.GetPagination(x => false, page, size);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.User.UserEndpoint)]
        [ProducesResponseType(typeof(Result<List<UserViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            Result<UserViewModel> result = await _userService.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost(ApiEndPointConstant.User.UsersEndpoint)]
        [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {   
            Result<UserViewModel> result = await _userService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut(ApiEndPointConstant.User.UserEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            Result<bool> result = await _userService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.User.UserEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _userService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }

/*        [HttpGet(ApiEndPointConstant.User.CoursesByCenterEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesByCenter(Guid id, int page = 1, int size = 100)
        {
            Result<List<CourseViewModel>> courses = await _userService.GetCoursesByCenterId(page, size, id);
            return Ok(courses);
        }*/

        [HttpGet(ApiEndPointConstant.User.CoursesByCenterEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesByCenter(Guid id, int page = 1, int size = 100)
        {
            PagingResult<CourseViewModel> courses = await _userService.GetCoursesByCenterId(page, size, id);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.User.StudentsEnpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsByCenter(Guid centerId, Guid? courseId)
        {
            Result<List<UserCourseDto>> courses = await _userService.GetStudentsByCenterId(centerId, courseId);
            return Ok(courses);
        }

        [HttpGet(ApiEndPointConstant.User.CoursesByUserEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesByUser(Guid id)
        {
            Result<List<CourseDto>> courses = await _userService.GetCoursesByUserId(id);
            return Ok(courses);
        }
    }
}
