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
                    Email = request.Email,
                    ImageUrl = request.ImageUrl,
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
                user.IsDeleted = true;
                _unitOfWork.GetRepository<User>().UpdateAsync(user);
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
                ICollection<User> users = await _unitOfWork.GetRepository<User>().GetListAsync(predicate: x => x.IsDeleted == false);
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
                    .GetPagingListAsync(include: x => x.Include(x => x.Role), predicate: x => x.IsDeleted == false);
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

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CenterId.Equals(id) && x.IsDeleted == false);

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }

        public async Task<(Tuple<string, Guid>, Result<RegisterResponse>, User user)> Register(RegisterRequest request)
        {
            var listUser = await _unitOfWork.GetRepository<User>().GetListAsync(predicate: x => x.UserName == request.UserName);

            if (listUser.Any())
            {
                throw new Exception(MessageConstant.Vi.User.Fail.UserNameExisted);
            }

            User newUser = new User()
            {
                UserName = request.UserName,
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                Status = request.UserStatus,
                Description = request.Description!,
                ImageUrl = request.ImageUrl,
                RoleId = request.RoleId,
            };
            try
            {
                User user = await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
                if (user == null)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.CreateUser);
                }
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.CreateUser);
                }
                user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(user.Id), include: x => x.Include(x => x.Role));

                RoleEnum role = EnumHelper.ParseEnum<RoleEnum>(user.Role.RoleName);
                Tuple<string, Guid> guidClaim = null!;
                RegisterResponse registerResponse = null!;

                registerResponse = new RegisterResponse(user.Id, user.UserName, user.FullName, user.Role.RoleName, user.Status.GetDescriptionFromEnum());

                return (guidClaim, Success(registerResponse), user);
            }
            catch (Exception ex)
            {
                return (null, new Result<RegisterResponse>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                }, null)!;
            }
        }

        public async Task<Result<bool>> ForgetPassword(ForgetPasswordRequest request)
        {
            try
            {
                if(request.NewPassword != request.ConfirmPassword)
                {
                    throw new Exception(MessageConstant.Vi.Auth.PasswordNotMatched);      
                }
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.UserName.Equals(request.Username));
                if(user == null)
                {
                    throw new ArgumentException(MessageConstant.Vi.User.Fail.NotFoundUser);
                }
                User newUser = new User()
                {
                    UserName = user.UserName,
                    Password = request.NewPassword,
                    FullName = user.FullName,
                    Email = user.Email,
                    Status = user.Status,
                    Description = user.Description!,
                    ImageUrl = user.ImageUrl,
                    RoleId = user.RoleId,
                };
                User createdUser = await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
                if (createdUser == null)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.CreateUser);
                }
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.User.Fail.CreateUser);
                }
                return Success(true);
            }
            catch (Exception ex) {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<List<UserCourseDto>>> GetStudentsByCenterId(Guid centerId, Guid? courseId)
        {
            User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(centerId));
            if (center == null) return BadRequest<List<UserCourseDto>>(MessageConstant.Vi.User.Fail.NotFoundCenter);

            ICollection<UserCourseDto> students = await _unitOfWork.GetRepository<UserCourse>().GetListAsync(
                selector: x => new UserCourseDto(x.User.UserName, x.User.FullName, x.User.Description),
                predicate: x => x.Course.CenterId.Equals(centerId),
                include: x => x.Include(x => x.User)
                );

            if (courseId != null)
            {
                students = await _unitOfWork.GetRepository<UserCourse>().GetListAsync(

                selector: x => new UserCourseDto(x.User.UserName, x.User.FullName, x.User.Description),
                        predicate: x => x.Course.Id.Equals(courseId),
                        include: x => x.Include(x => x.Course)
                    );
            }

            return Success(_mapper.Map<List<UserCourseDto>>(students));
        }

        public async Task<Result<List<CourseDto>>> GetCoursesByUserId(Guid userId)
        {
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(userId));
            if (user == null) return BadRequest<List<CourseDto>>(MessageConstant.Vi.User.Fail.NotFoundUser);

            ICollection<CourseDto> courses = await _unitOfWork.GetRepository<UserCourse>().GetListAsync(
                selector: x => new CourseDto(x.Course.Title, x.Course.Description, x.Course.Duration, x.Course.Tuitionfee),
                predicate: x => x.UserId.Equals(userId),
                include: x => x.Include(x => x.Course)
                );

            return Success(courses.ToList());
        }
    }
}
