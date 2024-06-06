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
        public async Task<IActionResult> Checkout([FromQuery] CreatePaymentRequest request)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                List<ItemData> items = new List<ItemData>();

                var course = _service.GetCourseById(request.CourseId);
                ItemData data = new ItemData(course.Result.Data.Title, 1, (int)course.Result.Data.TuitionFee);
                items.Add(data);
                var tutionFee = course.Result.Data.TuitionFee;

                var baseUrl = "https://localhost:44315";
                var successUrl = $"{baseUrl}{ApiEndPointConstant.UserCourse.CourseUserEndpointJoin}?request={request}";
                var cancelUrl = "https://.app/payment/cancel";
                PaymentData paymentData = new PaymentData(orderCode, (int)tutionFee, "Thanh toan hoc phi", items, cancelUrl, successUrl);
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
                return Redirect("https://mixed-food.vercel.app");
            }
        }
    }
}
