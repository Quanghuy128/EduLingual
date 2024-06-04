using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
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
        public PayOsController(ILogger<PayOsController> logger, PayOS payOs) : base(logger)
        {
            _payOs = payOs;
        }
        [HttpPost(ApiEndPointConstant.PayOs.PayOsEndpoint)]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                List<ItemData> items = new List<ItemData>();
                PaymentData paymentData = new PaymentData(1, 1000, "Thanh toan don hang", items, "https://mixed-food.vercel.app/payment/cancel", "https://mixed-food.vercel.app/payment/success");
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
