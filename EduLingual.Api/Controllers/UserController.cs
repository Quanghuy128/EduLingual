using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.User;
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
        public async Task<IActionResult> GetAll()
        {
            List<UserViewModel> users = await _userService.GetAll(x => false);
            return Ok(users);
        }
    }
}
