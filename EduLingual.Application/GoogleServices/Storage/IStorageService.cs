using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace EduLingual.Application.GoogleServices.Storage
{
    public interface IStorageService
    {
        Task<Result<FileViewModel>> UploadFile(IFormFile file);
    }
}
