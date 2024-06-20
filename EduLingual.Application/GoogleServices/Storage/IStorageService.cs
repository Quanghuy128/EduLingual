using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using DataObject = Google.Apis.Storage.v1.Data;

namespace EduLingual.Application.GoogleServices.Storage
{
    public interface IStorageService
    {
        Task<Result<FileViewModel>> UploadFile(IFormFile file);
        public Task<(Result<FileViewModel>, MemoryStream)> DownloadFile(string fileName);
    }
}
