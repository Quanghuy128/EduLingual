using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

                _unitOfWork.GetRepository<Course>().DeleteAsync(course);
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
                    Status = request.CourseStatus ?? course.Status
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
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

            if (course == null) return BadRequest<CourseViewModel>(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            return Success(_mapper.Map<CourseViewModel>(course));
        }

        public Task<Result<CourseViewModel>> GetCourseByCondition(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<CourseViewModel>>> GetCourses(CourseFilter courseFilter)
        {
            CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.AreaName));
            if (courseArea == null) return BadRequest<List<CourseViewModel>>(MessageConstant.Vi.CourseArea.Fail.NotFoundCourseArea);

            CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.LanguageName));
            if (courseLanguage == null) return BadRequest<List<CourseViewModel>>(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

            CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.CategoryName));
            if (courseCategory == null) return BadRequest<List<CourseViewModel>>(MessageConstant.Vi.CourseCategory.Fail.NotFoundCourseCategory);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(
                predicate: x => x.CourseArea.Name.Equals(courseFilter.AreaName) &&
                                x.CourseLanguage.Name.Equals(courseFilter.LanguageName) &&
                                x.CourseCategory.Name.Equals(courseFilter.CategoryName),
                include: x => x.Include(x => x.CourseArea)
                               .Include(x => x.CourseLanguage)
                               .Include(x => x.CourseCategory)
                               .Include(x => x.Center)
                );

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }

        public async Task<PagingResult<CourseViewModel>> GetPagination(Expression<Func<Course, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<Course> courses = await _unitOfWork.GetRepository<Course>().GetPagingListAsync(
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

        public async Task<Result<List<CourseViewModel>>> GetCoursesByCenterId(Guid id)
        {
            User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            if (center == null) return BadRequest<List<CourseViewModel>>(MessageConstant.Vi.User.Fail.NotFoundCenter);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CenterId.Equals(id));

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }


    }
}
