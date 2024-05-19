using EduLingual.Api.Helpers;
using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
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

        public async Task<Result<UserViewModel>> Create(CreateUserRequest request)
        {
            try
            {
                User user = new User()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    FullName = request.FullName,
                    Description = request.Description ?? string.Empty,
                    Status = request.UserStatus,
                    RoleId = request.RoleId,    
                };
                User result = await _unitOfWork.GetRepository<User>().InsertAsync(user);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.CreateUser);
                }
                return Success(_mapper.Map<UserViewModel>(result));
            }catch (Exception ex) {
                return Fail<UserViewModel>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));       
                _unitOfWork.GetRepository<User>().DeleteAsync(user);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.DeleteUser);
                }
                return Success(isSuccessful);  
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<UserViewModel>> Get(Guid id)
        {
            try
            {
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return Success(_mapper.Map<UserViewModel>(user));
            }
            catch (Exception ex)
            {
                return BadRequest<UserViewModel>(ex.Message);
            }
        }

        public async Task<Result<List<UserViewModel>>> GetAll(Expression<Func<User, bool>>? predicate)
        {
            try
            {
                ICollection<User> users = await _unitOfWork.GetRepository<User>().GetListAsync();
                return Success(_mapper.Map<List<UserViewModel>>(users));
            }catch (Exception ex) {
                return BadRequest<List<UserViewModel>>(ex.Message);
            }
        }

        public Task<Result<UserViewModel>> GetByCondition(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<User, bool>>? predicate, int page, int size)
        {
            try
            {

                IPaginate<User> users = 
                    await _unitOfWork.GetRepository<User>()
                    .GetPagingListAsync();
                return SuccessWithPaging<UserViewModel>(
                        _mapper.Map<IPaginate<UserViewModel>>(users), 
                        page, 
                        size, 
                        users.Total);   
            }catch (Exception ex) {
            }
            return null!;
        }

        public Task<Result<UserViewModel>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> Update(Guid id, UpdateUserRequest request)
        {
            try
            {
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                User newUser = new User()
                {
                    Id = id,
                    UserName = request.UserName ?? user.UserName,
                    Password = request.Password ?? user.Password,
                    FullName = request.FullName ?? user.FullName,
                    Description = request.Description ?? user.Description,
                    Status = request.UserStatus ?? user.Status,
                    RoleId = request.RoleId ?? user.RoleId  
                };
                _unitOfWork.GetRepository<User>().UpdateAsync(newUser);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.UpdateUser);
                }
                    return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }
    }
}
