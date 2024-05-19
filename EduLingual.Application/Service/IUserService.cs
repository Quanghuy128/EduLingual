using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using System.Linq.Expressions;
using System.Security.Principal;

namespace EduLingual.Application.Service
{
    public interface IUserService
    {
        Task<Result<UserViewModel>> Login(string username, string password);
        Task<Result<List<UserViewModel>>> GetAll(Expression<Func<UserDto, bool>>? predicate);
        Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<UserDto, bool>>? predicate);
        Task<Result<UserViewModel>> Get(Guid id);
        Task<Result<UserViewModel>> GetByCondition(Expression<Func<UserDto, bool>> predicate);
        Task<Result<UserViewModel>> Create(CreateUserRequest request);
        Task<Result<bool>> Update(UpdateUserRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
