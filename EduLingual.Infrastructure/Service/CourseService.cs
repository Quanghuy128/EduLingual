using EduLingual.Application.Extensions;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public class CourseService : BaseService<CourseService>, ICourseService
    {

        public CourseService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Result<CourseViewModel>> Create(CreateCourseRequest request)
        {
            try
            {
                User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CenterId));
                if (center == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.User.Fail.NotFoundCenter);

                CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseAreaId));
                if (courseArea == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.CourseArea.Fail.NotFoundCourseArea);

                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseLanguageId));
                if (courseLanguage == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

                CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseCategoryId));
                if (courseCategory == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.CourseCategory.Fail.NotFoundCourseCategory);

                Course newCourse = new Course()
                {
                    Title = request.Title,
                    Description = request.Description,
                    Duration = request.Duration,
                    Tuitionfee = request.Tuitionfee,
                    CenterId = request.CenterId,
                    CourseAreaId = request.CourseAreaId,
                    CourseLanguageId = request.CourseLanguageId,
                    CourseCategoryId = request.CourseCategoryId,
                };

                Course result = await _unitOfWork.GetRepository<Course>().InsertAsync(newCourse);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Course.Fail.CreateCourse);
                }

                return Success(_mapper.Map<CourseViewModel>(result));
            }
            catch (Exception ex)
            {
                return Fail<CourseViewModel>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                course.IsDeleted = true;

                _unitOfWork.GetRepository<Course>().UpdateAsync(course);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Course.Fail.DeleteCourse);
                }

                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }
        public async Task<Result<bool>> Update(Guid id, UpdateCourseRequest request)
        {
            try
            {
                Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CenterId));
                if (center == null) return BadRequest<bool>(MessageConstant.Vi.User.Fail.NotFoundCenter);

                CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseAreaId));
                if (courseArea == null) return BadRequest<bool>(MessageConstant.Vi.CourseArea.Fail.NotFoundCourseArea);

                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseLanguageId));
                if (courseLanguage == null) return BadRequest<bool>(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

                CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseCategoryId));
                if (courseCategory == null) return BadRequest<bool>(MessageConstant.Vi.CourseCategory.Fail.NotFoundCourseCategory);

                Course newCourse = new Course()
                {
                    Id = id,
                    Title = request.Title ?? course.Title,
                    Description = request.Description ?? course.Description,
                    Duration = request.Duration ?? course.Duration,
                    Tuitionfee = request.Tuitionfee ?? course.Tuitionfee,
                    Center = center ?? course.Center,
                    CourseArea = courseArea ?? course.CourseArea,
                    CourseLanguage = courseLanguage ?? course.CourseLanguage,
                    CourseCategory = courseCategory ?? course.CourseCategory,
                    Status = request.Status ?? course.Status,
                    CreatedAt = course.CreatedAt,
                };

                _unitOfWork.GetRepository<Course>().UpdateAsync(newCourse);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Course.Fail.UpdateCourse);
                }
                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<CourseViewModel>> GetCourseById(Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(
                    predicate: x => x.Id.Equals(id),
                    include: x => x.Include(x => x.Center)
                                   .Include(x => x.CourseArea)
                                   .Include(x => x.CourseLanguage)
                                   .Include(x => x.CourseCategory)
                );

            if (course == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            return Success(_mapper.Map<CourseViewModel>(course));
        }

        public async Task<Result<List<CourseViewModel>>> GetCourses(string? title, CourseFilter? courseFilter, string? sort)
        {
            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(
                    predicate: BuildGetSearchCoursesQuery(title, courseFilter, sort),
                    orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                    include: x => x.Include(x => x.Center)
                                   .Include(x => x.CourseArea)
                                   .Include(x => x.CourseLanguage)
                                   .Include(x => x.CourseCategory)
                );

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }

        private Expression<Func<Course, bool>> BuildGetSearchCoursesQuery(string? title, CourseFilter? filter, string? sort)
        {
            Expression<Func<Course, bool>> filterQuery = x => x.Status.Equals(CourseStatus.Active) && x.IsDeleted == false;
            if (title != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.Title.Trim().ToLower().Contains(title));
            }
            if (filter.AreaId != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.CourseAreaId.Equals(filter.AreaId));
            }
            if (filter.CategoryId != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.CourseCategoryId.Equals(filter.CategoryId));
            }
            if (filter.LanguageId != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.CourseLanguageId.Equals(filter.LanguageId));
            }

            return filterQuery;
        }

        public async Task<PagingResult<CourseViewModel>> GetPagination(int page, int size, string? title, CourseStatus? status, Guid? centerId)
        {
            try
            {
                IPaginate<Course> courses = await _unitOfWork.GetRepository<Course>().GetPagingListAsync(
                    predicate: BuildGetCoursesQuery(title, status, centerId),
                    orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                    include: x => x.Include(x => x.CourseArea)
                                  .Include(x => x.CourseLanguage)
                                  .Include(x => x.CourseCategory)
                                  .Include(x => x.Center)
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

        private Expression<Func<Course, bool>> BuildGetCoursesQuery(string? title, CourseStatus? status, Guid? centerId)
        {
            Expression<Func<Course, bool>> filterQuery = x => x.IsDeleted == false;
            if (title != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.Title.Trim().ToLower().Contains(title));
            }
            if (status != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.Status.Equals(status));
            }
            if (centerId != null)
            {
                filterQuery = filterQuery.AndAlso(x => x.CenterId.Equals(centerId));
            }

            return filterQuery;
        }

        public async Task<Result<List<UserCourseDto>>> GetStudentsByCourse(Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            if (course == null) return BadRequest<List<UserCourseDto>>(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            ICollection<UserCourseDto> students = await _unitOfWork.GetRepository<UserCourse>().GetListAsync(
                selector: x => new UserCourseDto(x.User.UserName, x.User.FullName, x.User.Description),
                predicate: x => x.CourseId.Equals(id),
                include: x => x.Include(x => x.User)
                );

            return Success(_mapper.Map<List<UserCourseDto>>(students));
        }
    }
}
