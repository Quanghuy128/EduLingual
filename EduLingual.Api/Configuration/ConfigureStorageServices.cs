using EduLingual.Domain.Common;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureStorageServices
    {
        public static IServiceCollection RegisterStorageService(this IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var googleCredential = GoogleCredential.FromFile(AppConfig.GoogleSetting.CredentialFile);
                var storage = StorageClient.Create(googleCredential);
                return storage;
            });
            return services;
        }
    }
}
