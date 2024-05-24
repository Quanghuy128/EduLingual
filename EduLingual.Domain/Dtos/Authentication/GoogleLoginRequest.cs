using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Dtos.Authentication
{
    public class GoogleLoginRequest
    {
        [Required]
        public string AccessToken { get; set; } 
    }
}
