

namespace EduLingual.Domain.Common
{
    public class AppConfig
    {
        public static ConnectionString ConnectionString { get; set; } = null!;
        public static JwtSetting JwtSetting { get; set; } = null!;
        public static GoogleSetting GoogleSetting { get; set; } = null!;
        public static PayOSSetting PayOSSetting { get; set; } = null!;
    }
    public class ConnectionString
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
    public class JwtSetting
    {
        public string SecretKey { get; set; } = "Secret Key";         
        public bool ValidateIssuerSigningKey{ get; set;}
        public string? IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
    }
    public class GoogleSetting
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string CredentialFile {  get; set; } = string.Empty;
        public string StorageBucket {  get; set; } = string.Empty;
    }
    public class PayOSSetting
    {
        public string PAYOS_CHECKSUM_KEY { get; set; } = string.Empty;
        public string PAYOS_API_KEY { get; set; } = string.Empty;
        public string PAYOS_CLIENT_ID { get; set; } = string.Empty;
    }
}
