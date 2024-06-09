using EduLingual.Application.GoogleServices.Storage;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.Feedback;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{

    public class MediaController : BaseController<MediaController>
    {
        private IStorageService _storageService;
        public MediaController(IStorageService storageService, ILogger<MediaController> logger) : base(logger)
        {
            _storageService = storageService;
        }

        [HttpPost(ApiEndPointConstant.Media.UploadEndpoint)]
        [ProducesResponseType(typeof(Result<FileViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            Result<FileViewModel> result = await _storageService.UploadFile(file);
            return Ok(result);
        }
    }
}
