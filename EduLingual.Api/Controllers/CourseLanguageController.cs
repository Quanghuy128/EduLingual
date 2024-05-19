using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
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

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }
    }
}
