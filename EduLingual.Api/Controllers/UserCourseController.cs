using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.UserCourse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly IUserCourseService _userCourseService;

        public UserCourseController(IUserCourseService userCourseService)
        {
            _userCourseService = userCourseService;
        }
        [HttpPost(ApiEndPointConstant.UserCourse.CourseUserEndpointJoin)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserJoinCourse(UserCourseRequest request)
        {
            Result<bool> result = await _userCourseService.UserJoinCourseAsync(request);
            return Ok(result);
        }
    }
}
