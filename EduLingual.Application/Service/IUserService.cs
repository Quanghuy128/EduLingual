using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using System.Linq.Expressions;
using System.Security.Principal;

namespace EduLingual.Application.Service
{
    public interface IUserService
    {
        Task<UserViewModel> Login(string username, string password);
        Task<List<UserViewModel>> GetAll(Expression<Func<UserDto, bool>>? predicate);
        Task<IPaginate<UserViewModel>> GetPagination(Expression<Func<UserDto, bool>>? predicate);
        Task<UserViewModel> Get(Guid id);
        Task<UserViewModel> GetByCondition(Expression<Func<UserDto, bool>> predicate);
        Task<UserViewModel> Create(CreateUserRequest request);
        Task<bool> Update(UpdateUserRequest request);
        Task<bool> Delete(Guid id);
    }
}
