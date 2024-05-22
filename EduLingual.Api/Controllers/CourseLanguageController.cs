using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseLanguageController : BaseController<CourseLanguageController>
    {
        private readonly ICourseLanguageService _courseLanguageService;

        public CourseLanguageController(ILogger<CourseLanguageController> logger, ICourseLanguageService courseLanguageService) : base(logger)
        {
            _courseLanguageService = courseLanguageService;
        }

        [HttpGet(ApiEndPointConstant.CourseLanguage.CourseLanguagesEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseLanguageViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page, [FromQuery] int size)
        {
            PagingResult<CourseLanguageViewModel> result = await _courseLanguageService.GetPagination(x => false, page, size);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.CourseLanguage.CourseLanguageEndpoint)]
        [ProducesResponseType(typeof(Result<CourseLanguageViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            Result<CourseLanguageViewModel> result = await _courseLanguageService.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost(ApiEndPointConstant.CourseLanguage.CourseLanguagesEndpoint)]
        [ProducesResponseType(typeof(Result<CourseLanguageViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCourseLanguageRequest request)
        {
            Result<CourseLanguageViewModel> result = await _courseLanguageService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut(ApiEndPointConstant.CourseLanguage.CourseLanguageEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseLanguageRequest request)
        {
            Result<bool> result = await _courseLanguageService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.CourseLanguage.CourseLanguageEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _courseLanguageService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
