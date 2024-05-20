using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
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

        public Task<Result<CourseViewModel>> Create(CreateCourseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
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
                                x.CourseCategory.Name.Equals(courseFilter.CategoryName)
                );

            return Success(_mapper.Map<List<CourseViewModel>>(courses));
        }

        public Task<Result<bool>> Update(UpdateCourseRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<CoursesByCenterViewModel>> GetCoursesByCenterId(Guid id)

        {
            User center = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

            if (center == null) return BadRequest<CoursesByCenterViewModel>(MessageConstant.Vi.User.Fail.NotFoundCenter);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CenterId.Equals(id));

            CoursesByCenterViewModel coursesByCenterViewModel = new CoursesByCenterViewModel()
            {
                FullName = center.FullName,
                Description = center.Description,
                Courses = _mapper.Map<List<CourseViewModel>>(courses)
            };

            return Success(coursesByCenterViewModel);
        }
    }
}
