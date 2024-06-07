using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.Feedback;
using EduLingual.Domain.Entities;
using EduLingual.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : BaseController<FeedbackController>
    {
        private readonly IFeedbackService _feedbackSerivce;
        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackService feedbackService) : base(logger)
        {
            _feedbackSerivce = feedbackService;
        }

        [HttpGet(ApiEndPointConstant.Feedback.FeedbacksPaginationEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page, [FromQuery] int size)
        {
            PagingResult<FeedbackViewModel> result = await _feedbackSerivce.GetPagination(x => false, page, size);
            return Ok(result);
        }
        [HttpGet(ApiEndPointConstant.Feedback.FeedbacksEndpoint)]
        [ProducesResponseType(typeof(Result<CourseViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            Result<FeedbackViewModel> result = await _feedbackSerivce.Get(id);
            return Ok(result);
        }
        [HttpPost(ApiEndPointConstant.Feedback.FeedbackEndpoint)]
        [ProducesResponseType(typeof(Result<FeedbackViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackRequest request)
        {
            Result<FeedbackViewModel> result = await _feedbackSerivce.Create(request);
            return Ok(result);
        }

        [HttpPut(ApiEndPointConstant.Feedback.FeedbackEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFeedbackRequest request)
        {
            Result<bool> result = await _feedbackSerivce.Update(id, request);
            return Ok(result);
        }

        [HttpDelete(ApiEndPointConstant.Feedback.FeedbackEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _feedbackSerivce.Delete(id);
            return Ok(result);
        }
    }
}
