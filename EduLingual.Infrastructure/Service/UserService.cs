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
using Microsoft.IdentityModel.Tokens;
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
            }
            catch (Exception ex)
            {
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

        public async Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<User, bool>>? predicate, int page, int size)
        {
            try
            {

                IPaginate<User> users =
                    await _unitOfWork.GetRepository<User>()
                    .GetPagingListAsync(
                            predicate: x => x.IsDeleted == false,
                            include: x => x.Include(x => x.Role),
                            orderBy: x => x.OrderByDescending(x => x.Status).ThenByDescending(x => x.CreatedAt),
                            page: page,
                            size: size
                        );
                return SuccessWithPaging<UserViewModel>(
                        _mapper.Map<IPaginate<UserViewModel>>(users),
                        page,
                        size,
                        users.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<(Tuple<string, Guid>, Result<LoginResponse>, User user)> Login(LoginRequest request)
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
                    ImageUrl = request.ImageUrl ?? user.ImageUrl,
                    Email = request.Email ?? user.Email,
                    Status = request.Status ?? user.Status,
                    RoleId = request.RoleId ?? user.RoleId
                };

                newUser.CreatedAt = user.CreatedAt;
                newUser.UpdatedAt = DateTime.Now.AddHours(7);

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
            };

            newUser.RoleId = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(
                selector: x => x.Id,
                predicate: x => x.RoleName.Equals("User"));
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
                if (request.NewPassword != request.ConfirmPassword)
                {
                    throw new Exception(MessageConstant.Vi.Auth.PasswordNotMatched);
                }
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.UserName.Equals(request.Username));
                if (user == null)
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
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<PagingResult<UserCourseDto>> GetStudentsByCenterId(int page, int size, Guid centerId, Guid? courseId)
        {
            try
            {
                User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(centerId));
                if (center == null) return NotFounds<UserCourseDto>(MessageConstant.Vi.User.Fail.NotFoundCenter);

                IPaginate<UserCourseDto> students = await _unitOfWork.GetRepository<UserCourse>().GetPagingListAsync(
                    selector: x => new UserCourseDto(x.User.UserName, x.User.FullName, x.User.Description, x.JoinedAt, x.Course.Title),
                    predicate: courseId is null ? x => x.Course.CenterId.Equals(centerId) : x => x.Course.CenterId.Equals(centerId) && x.Course.Id.Equals(courseId),
                    include: x => x.Include(x => x.User).Include(x => x.Course)
                );

                return SuccessWithPaging<UserCourseDto>(
                        _mapper.Map<IPaginate<UserCourseDto>>(students),
                        page,
                        size,
                        students.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<PagingResult<CourseViewModel>> GetCoursesByCenterId(int page, int size, Guid id)
        {
            try
            {
                User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (center == null) return NotFounds<CourseViewModel>(MessageConstant.Vi.User.Fail.NotFoundCenter);

                IPaginate<Course> courses = await _unitOfWork.GetRepository<Course>().GetPagingListAsync(
                            predicate: x => x.CenterId.Equals(id) && x.IsDeleted == false,
                            include: x => x.Include(x => x.Center)
                                           .Include(x => x.CourseArea)
                                           .Include(x => x.CourseLanguage)
                                           .Include(x => x.CourseCategory),
                            page: page,
                            size: size
                        );

                return SuccessWithPaging<CourseViewModel>(
                        _mapper.Map<IPaginate<CourseViewModel>>(courses),
                        page,
                        size,
                        courses.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<PagingResult<CourseByUserDto>> GetCoursesByUserId(int page, int size, Guid userId)
        {
            try
            {
                User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(userId));
                if (user == null) return NotFounds<CourseByUserDto>(MessageConstant.Vi.User.Fail.NotFoundUser);

                IPaginate<CourseByUserDto> courses = await _unitOfWork.GetRepository<UserCourse>().GetPagingListAsync(
                    selector: x => new CourseByUserDto(x.Course.Id, x.Course.Title, x.Course.Description, x.Course.Duration, x.Course.Tuitionfee, x.Course.Center.FullName),
                    predicate: x => x.UserId.Equals(userId),
                    include: x => x.Include(x => x.User)
                                   .Include(x => x.Course).ThenInclude(x => x.Center)
                );

                return SuccessWithPaging<CourseByUserDto>(
                        _mapper.Map<IPaginate<CourseByUserDto>>(courses),
                        page,
                        size,
                        courses.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }
    }
}
