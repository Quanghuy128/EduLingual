using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseCategory;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : BaseController<CourseCategoryController>
    {
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoryController(ILogger<CourseCategoryController> logger, ICourseCategoryService courseCategoryService) : base(logger)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet(ApiEndPointConstant.CourseCategory.CourseCategoriesEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseCategoryViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page, [FromQuery] int size)
        {
            PagingResult<CourseCategoryViewModel> result = await _courseCategoryService.GetPagination(x => false, page, size);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet(ApiEndPointConstant.CourseCategory.CourseCategoryEndpoint)]
        [ProducesResponseType(typeof(Result<List<CourseCategoryViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            Result<CourseCategoryViewModel> result = await _courseCategoryService.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost(ApiEndPointConstant.CourseCategory.CourseCategoriesEndpoint)]
        [ProducesResponseType(typeof(Result<CourseCategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCourseCategoryRequest request)
        {
            Result<CourseCategoryViewModel> result = await _courseCategoryService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut(ApiEndPointConstant.CourseCategory.CourseCategoryEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseCategoryRequest request)
        {
            Result<bool> result = await _courseCategoryService.Update(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete(ApiEndPointConstant.CourseCategory.CourseCategoryEndpoint)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Result<bool> result = await _courseCategoryService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
