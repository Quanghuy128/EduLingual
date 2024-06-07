using EduLingual.Api.Utils;
using EduLingual.Application.GoogleServices.Auth;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Entities;
using EduLingual.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduLingual.Api.Controllers
{
    public class AuthenticationController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        private readonly IGoogleService _googleService;

        public AuthenticationController(ILogger<UserController> logger, IUserService userService, IGoogleService googleService) : base(logger)
        {
            _userService = userService;
            _googleService = googleService;
        }
        [HttpPut(ApiEndPointConstant.Authentication.LoginEndPoint)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            (Tuple<string, Guid>, Result<LoginResponse>, User) result = await _userService.Login(request);
            if(result.Item2.Data != null)
            {
                var token = JwtUtil.GenerateJwtToken(result.Item3, result.Item1);
                result.Item2.Data!.AccessToken = token;
            }
            return StatusCode((int)result.Item2.StatusCode, result.Item2);
        }

        [HttpPut(ApiEndPointConstant.Authentication.GoogleLoginEndPoint)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            (Tuple<string, Guid>, Result<LoginResponse>, User) result = (await _googleService.GoogleLogin(request.AccessToken)).Data;

            var token = JwtUtil.GenerateJwtToken(result.Item3, result.Item1);
            result.Item2.Data!.AccessToken = token;
            return StatusCode((int)result.Item2.StatusCode, result.Item2);
        }

        [HttpPut(ApiEndPointConstant.Authentication.RegisterEndPoint)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            (Tuple<string, Guid>, Result<RegisterResponse>, User) result = await _userService.Register(request);
            if (result.Item2.Data != null)
            {
                var token = JwtUtil.GenerateJwtToken(result.Item3, result.Item1);
                result.Item2.Data!.AccessToken = token;
            }
            return StatusCode((int)result.Item2.StatusCode, result.Item2);
        }
    }
}
