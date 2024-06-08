using EduLingual.Application.GoogleServices.Storage;
using EduLingual.Application.Repository;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos;
using EduLingual.Infrastructure.Service;
using Google.Cloud.Storage.V1;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EduLingual.Infrastructure.GoogleServices.Storage
{
    public class StorageService : BaseService<StorageService>, IStorageService
    {
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public StorageService(StorageClient storageClient, IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<StorageService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            this.storageClient = storageClient;
            this.bucketName = AppConfig.GoogleSetting.StorageBucket;
        }

        public async Task<Result<FileViewModel>> UploadFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                Guid id = Guid.NewGuid();
                string extension = Path.GetExtension(file.FileName);
                string objectName = $"{id}{extension}";
                try { await storageClient.UploadObjectAsync(bucketName, objectName, null, memoryStream); }
                catch (Exception e)
                {
                    return Fail<FileViewModel>(e.Message);
                }
                return Success(new FileViewModel
                {
                    Name = objectName,
                    Url = $"https://storage.googleapis.com/{bucketName}/{objectName}"
                });
            }
        }
    }
}
