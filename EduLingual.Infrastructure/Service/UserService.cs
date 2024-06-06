using EduLingual.Api.Helpers;
using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net;
using System.Security.Principal;

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
                    .GetPagingListAsync(include: x => x.Include(x => x.Role));
                return SuccessWithPaging<UserViewModel>(
                        _mapper.Map<IPaginate<UserViewModel>>(users), 
                        page, 
                        size, 
                        users.Total);   
            }catch (Exception ex) {
            }
            return null!;
        }

        public async Task<(Tuple<string, Guid> , Result<LoginResponse>, User user)> Login(LoginRequest request)
        {
            Expression<Func<User, bool>> searchFilter = p =>
                                                p.UserName.Equals(request.Username) &&
                                                p.Password.Equals(request.Password);
            User user = await _unitOfWork.GetRepository<User>()
                .SingleOrDefaultAsync(predicate: searchFilter, include: p => p.Include(x => x.Role));

            if (user == null) return (null, new Result<LoginResponse>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = MessageConstant.Vi.User.Fail.NotFoundUser
            }, null)!;
            RoleEnum role = EnumHelper.ParseEnum<RoleEnum>(user.Role.RoleName);
            Tuple<string, Guid> guidClaim = null!;
            LoginResponse loginResponse = null!;

            loginResponse = new LoginResponse(user.Id, user.UserName, user.FullName, user.Role.RoleName, user.Status.GetDescriptionFromEnum());

            return (guidClaim, Success(loginResponse), user);
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

        public async Task<Result<List<CourseViewModel>>> GetCoursesByCenterId(Guid id)
        {
            User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            if (center == null) return BadRequest<List<CourseViewModel>>(MessageConstant.Vi.User.Fail.NotFoundCenter);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CenterId.Equals(id));

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }
    }
}
