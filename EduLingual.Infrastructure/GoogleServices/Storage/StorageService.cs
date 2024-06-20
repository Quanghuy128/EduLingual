using EduLingual.Application.GoogleServices.Storage;
using EduLingual.Application.Repository;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos;
using EduLingual.Infrastructure.Service;
using DataObject = Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
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
                DataObject.Object result = null!;
                try
                {
                    result = await storageClient.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                }
                catch (Exception e)
                {
                    return Fail<FileViewModel>(e.Message);
                }
                return Success(new FileViewModel
                {
                    Name = result.Name,
                    MediaLink = result.MediaLink,
                    SelfLink = result.SelfLink,
                    PublicLink = $"https://storage.cloud.google.com/{bucketName}/{objectName}",
                    Size = result.Size ?? 0,
                    Metadata = result.Metadata,
                    ContentType = result.ContentType
                });
            }
        }
        public async Task<(Result<FileViewModel>, MemoryStream)> DownloadFile(string fileName)
        {
            var memoryStream = new MemoryStream();
            DataObject.Object result = null!;
            try
            {
                result = await storageClient.DownloadObjectAsync(bucketName, fileName, memoryStream);
                memoryStream.Position = 0;
                return (Success(new FileViewModel
                {
                    Name = result.Name,
                    MediaLink = result.MediaLink,
                    SelfLink = result.SelfLink,
                    Size = result.Size ?? 0,
                    Metadata = result.Metadata,
                    ContentType = result.ContentType
                }), memoryStream);
            }
            catch (Exception e)
            {
                return (Fail<FileViewModel>(e.Message), null)!;
            }
        }
    }
}
