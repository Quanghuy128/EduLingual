namespace EduLingual.Api.Configuration
{
    public static class ConfigureResponseCompression
    {
        public static IServiceCollection AddeResponseCompression(this IServiceCollection services)
        {
            return services.AddResponseCompression(option =>
            {
                option.EnableForHttps = true;
            });
        }
    }
}
