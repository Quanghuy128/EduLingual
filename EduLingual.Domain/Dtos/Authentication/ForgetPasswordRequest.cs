using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Dtos.Authentication
{
    public class ForgetPasswordRequest
    {
        [Required]
        public string Username { get; set; }    
        [Required]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; } 
    }
}
