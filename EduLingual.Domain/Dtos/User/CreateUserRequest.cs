using EduLingual.Domain.Constants;
using EduLingual.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EduLingual.Domain.Dtos.User
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.User.Require.UserNameRequired)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstant.Vi.User.Require.PasswordRequired)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstant.Vi.User.Require.FullNameRequired)]
        public string FullName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public UserStatus UserStatus { get; set; } = UserStatus.Active;
    }
}
