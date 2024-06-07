
namespace EduLingual.Domain.Dtos.Authentication
{
    public class RegisterResponse : LoginResponse
    {
        public RegisterResponse(Guid id, string username, string name, string role, string status) : base(id, username, name, role, status)
        {
        }
    }
}
