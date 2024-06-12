using Autofac.Core;
using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOsController : BaseController<PayOsController>
    {
        private readonly PayOS _payOs;
        private readonly ICourseService _service;

        public PayOsController(ILogger<PayOsController> logger, PayOS payOs, ICourseService service) : base(logger)
        {
            _payOs = payOs;
            _service = service;
        }

        [HttpPost(ApiEndPointConstant.PayOs.PayOsEndpoint)]
        public async Task<IActionResult> Checkout([FromQuery] Guid userId, [FromQuery] Guid courseId, [FromQuery] string paymentMethod, [FromQuery] double fee, [FromQuery] string fullName, [FromQuery] string phoneNumber)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                var totalFee = 0.0;
                List<ItemData> items = new List<ItemData>();

                var course = _service.GetCourseById(courseId);
                ItemData data = new ItemData(course.Result.Data.Title, 1, (int)course.Result.Data.Tuitionfee);
                items.Add(data);
                totalFee += course.Result.Data.Tuitionfee;

                var baseUrl = "https://localhost:44315";
                var successUrl = $"{baseUrl}{ApiEndPointConstant.UserCourse.CourseUserEndpointJoin}?userId={userId}&courseId={courseId}&paymentMethod={paymentMethod}&fee={fee}&fullName={fullName}&phoneNumber={phoneNumber}";
                var cancelUrl = "https://.app/payment/cancel";
                PaymentData paymentData = new PaymentData(orderCode, (int)totalFee, "Thanh toan hoc phi", items, cancelUrl, successUrl, buyerName: fullName, buyerPhone: phoneNumber);
                CreatePaymentResult createPayment = await _payOs.createPaymentLink(paymentData);

                return Ok(new
                {
                    message = "redirect",
                    url = createPayment.checkoutUrl
                });
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Redirect("https://.app");
            }
        }
    }
}
