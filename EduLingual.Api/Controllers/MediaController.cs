using EduLingual.Application.GoogleServices.Storage;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos;
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
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            Result<FileViewModel> result = await _storageService.UploadFile(file);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet(ApiEndPointConstant.Media.DownloadEndpoint)]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        public async Task<IActionResult> Download([FromQuery] string fileName)
        {
            (Result<FileViewModel>, MemoryStream) result = await _storageService.DownloadFile(fileName);
            return File(result.Item2, result.Item1.Data!.ContentType, result.Item1.Data.Name);
        }
    }
}
