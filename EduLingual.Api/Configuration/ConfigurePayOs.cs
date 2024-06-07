
using EduLingual.Domain.Common;
using Net.payOS;

namespace EduLingual.Api.Configuration
{
    public static class ConfigurePayOs
    {
        public static IServiceCollection AddPayOs(this IServiceCollection services)
        {
            PayOS payos = new PayOS(AppConfig.PayOSSetting.PAYOS_CLIENT_ID,
                AppConfig.PayOSSetting.PAYOS_API_KEY,
                AppConfig.PayOSSetting.PAYOS_CHECKSUM_KEY);
            services.AddSingleton(payos);
            return services;
        }
    }
}
