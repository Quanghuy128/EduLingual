using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
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

        public Task<Result<UserViewModel>> Create(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserViewModel>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<UserViewModel>>> GetAll(Expression<Func<UserDto, bool>>? predicate)
        {
            try
            {
                ICollection<User> users = await _unitOfWork.GetRepository<User>().GetListAsync();
                return Success(_mapper.Map<List<UserViewModel>>(users));
            }catch (Exception ex) {
                return BadRequest<List<UserViewModel>>(ex.Message);
            }
            return null!;
        }

        public Task<Result<UserViewModel>> GetByCondition(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<UserDto, bool>>? predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserViewModel>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Update(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
