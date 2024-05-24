using EduLingual.Domain.Common;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureAppSettings
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {

            AppConfig.ConnectionString = new ConnectionString();
            AppConfig.JwtSetting = new JwtSetting();
            AppConfig.GoogleSetting = new GoogleSetting();

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionString);

            configuration.Bind("JwtSettings", AppConfig.JwtSetting);

            configuration.Bind("GoogleSetting", AppConfig.GoogleSetting);
        }
    }
}
