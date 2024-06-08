using Autofac.Core;
using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;

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
                ItemData data = new ItemData(course.Result.Data.Title, 1, (int)course.Result.Data.Tuitionfee);
                items.Add(data);
                var tutionFee = course.Result.Data.Tuitionfee;

                var baseUrl = "https://localhost:44315";
                string url = $"{baseUrl}{ApiEndPointConstant.UserCourse.CourseUserEndpointJoin}";

                HttpResponseMessage successUrl;

                MediaTypeHeaderValue typeHeaderValue = new MediaTypeHeaderValue("application/json");
                string json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, typeHeaderValue);


                using (HttpClient httpClient = new HttpClient())
                {
                     successUrl = await httpClient.PostAsync(url, httpContent);
                }

                //var successUrl = $"{baseUrl}{ApiEndPointConstant.UserCourse.CourseUserEndpointJoin}?request={request}";
                var cancelUrl = "https://.app/payment/cancel";
                PaymentData paymentData = new PaymentData(orderCode, (int)tutionFee, "Thanh toan hoc phi", items, cancelUrl, "");
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
