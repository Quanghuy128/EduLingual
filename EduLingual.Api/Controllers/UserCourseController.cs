using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Payment;
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
        private readonly IPaymentService _paymentService;

        public UserCourseController(IUserCourseService userCourseService, IPaymentService paymentService)
        {
            _userCourseService = userCourseService;
            _paymentService = paymentService;
        }

        [HttpGet(ApiEndPointConstant.UserCourse.CourseUserEndpointJoin)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserJoinCourse([FromQuery] Guid userId, [FromQuery] Guid courseId, [FromQuery] string paymentMethod, [FromQuery] double fee, [FromQuery] string fullName, [FromQuery] string phoneNumber, [FromQuery] string status)
        {
            if (status == "CANCELLED")
            {
                return Redirect("http://68.183.186.61:3000");
            }

            var request = new UserCourseRequest
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                PaymentMethod = paymentMethod,
                Fee = fee,
                UserId = userId,
                CourseId = courseId
            };

            Result<bool> result = await _userCourseService.UserJoinCourseAsync(request);

            if (!result.Data)
            {
                return Redirect(".app/payment/failure");
            }

            return Redirect(".app/payment/success");
        }
    }
}
