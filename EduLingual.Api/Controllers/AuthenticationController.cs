using EduLingual.Api.Utils;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace EduLingual.Api.Controllers
{
    public class AuthenticationController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<UserController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }
        [HttpPut(ApiEndPointConstant.Authentication.LoginEndPoint)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            (Tuple<string, Guid>, Result<LoginResponse>, User) result = await _userService.Login(request);

            var token = JwtUtil.GenerateJwtToken(result.Item3, result.Item1);
            result.Item2.Data!.AccessToken = token;
            return StatusCode((int)result.Item2.StatusCode, result.Item2);
        }
    }
}
