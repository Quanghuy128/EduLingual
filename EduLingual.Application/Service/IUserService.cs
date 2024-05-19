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
        Task<Result<List<UserViewModel>>> GetAll(Expression<Func<User, bool>>? predicate);
        Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<User, bool>>? predicate, int page, int size);
        Task<Result<UserViewModel>> Get(Guid id);
        Task<Result<UserViewModel>> GetByCondition(Expression<Func<User, bool>> predicate);
        Task<Result<UserViewModel>> Create(CreateUserRequest request);
        Task<Result<bool>> Update(Guid id, UpdateUserRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
