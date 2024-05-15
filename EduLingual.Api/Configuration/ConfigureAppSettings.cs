using EduLingual.Domain.Common;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureAppSettings
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {

            AppConfig.ConnectionString = new ConnectionString();

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionString);
        }
    }
}
