using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
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

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }
    }
}
