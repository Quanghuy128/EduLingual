using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Entities;

namespace EduLingual.Application.GoogleServices.Auth
{
    public interface IGoogleService
    {
        Task<Result<(Tuple<string, Guid>, Result<LoginResponse>, User)>> GoogleLogin(string token);
    }
}
