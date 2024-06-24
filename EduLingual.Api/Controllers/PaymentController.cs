using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController<PaymentController>
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService) : base(logger)
        {
            _paymentService = paymentService;
        }

        [HttpGet(ApiEndPointConstant.Payment.PaymentsEndpoint)]
        [ProducesResponseType(typeof(Result<List<PaymentViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? centerName, [FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<PaymentViewModel> result = await _paymentService.GetPagination(startDate, endDate, centerName, page, size);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.Payment.PaymentEndpoint)]
        [ProducesResponseType(typeof(Result<PaymentViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            Result<PaymentViewModel> course = await _paymentService.GetPaymentById(id);
            return Ok(course);
        }

        [HttpPut(ApiEndPointConstant.Payment.PaymentEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePaymentRequest request)
        {
            Result<bool> result = await _paymentService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.Payment.PaymentEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _paymentService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
