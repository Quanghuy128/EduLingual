using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseArea;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAreaController : BaseController<CourseAreaController>
    {
        private readonly ICourseAreaService _courseAreaService;

        public CourseAreaController(ILogger<CourseAreaController> logger, ICourseAreaService courseAreaService) : base(logger)
        {
            _courseAreaService = courseAreaService;
        }

        [HttpGet(ApiEndPointConstant.CourseArea.CourseAreasEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseAreaViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int size = 100)
        {
            PagingResult<CourseAreaViewModel> result = await _courseAreaService.GetPagination(x => false, page, size);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.CourseArea.CourseAreaEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseAreaViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            Result<CourseAreaViewModel> result = await _courseAreaService.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost(ApiEndPointConstant.CourseArea.CourseAreasEndpoint)]
        [ProducesResponseType(typeof(Result<CourseAreaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCourseAreaRequest request)
        {
            Result<CourseAreaViewModel> result = await _courseAreaService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut(ApiEndPointConstant.CourseArea.CourseAreaEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseAreaRequest request)
        {
            Result<bool> result = await _courseAreaService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.CourseArea.CourseAreaEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _courseAreaService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
