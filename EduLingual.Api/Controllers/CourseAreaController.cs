using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.User;
using Microsoft.AspNetCore.Http;
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

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {    
            return Ok();
        }
    }
}
