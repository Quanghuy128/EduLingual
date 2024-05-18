using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace EduLingual.Infrastructure.Service
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public Task<UserViewModel> Create(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserViewModel>> GetAll(Expression<Func<UserDto, bool>>? predicate)
        {
            ICollection<User> users = await _unitOfWork.GetRepository<User>().GetListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public Task<UserViewModel> GetByCondition(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IPaginate<UserViewModel>> GetPagination(Expression<Func<UserDto, bool>>? predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
