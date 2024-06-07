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
        [HttpPost(ApiEndPointConstant.UserCourse.CourseUserEndpointJoin)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserJoinCourse([FromQuery] CreatePaymentRequest request, [FromQuery] string status)
        {
            if (status == "CANCELLED")
            {
                return Redirect(".app/payment/cancel");
            }


            var userCourse = new UserCourseRequest
            {
                UserId = request.UserId,
                CourseId = request.CourseId
            };

            Result<bool> resultJoin = await _userCourseService.UserJoinCourseAsync(userCourse);
            Result<bool> resultPayment = await _paymentService.Create(request);

            if (!resultJoin.Data && !resultPayment.Data)
            {
                return Redirect(".app/payment/failure");
            }

            return Redirect(".app/payment/success");
        }
    }
}
