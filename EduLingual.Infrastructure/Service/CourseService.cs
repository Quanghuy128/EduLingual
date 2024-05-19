using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public class CourseService : BaseService<CourseService>, ICourseService
    {

        public CourseService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public Task<CourseViewModel> Create(CreateCourseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseViewModel> GetCourseById(Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

            if (course == null) throw new BadHttpRequestException(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            return _mapper.Map<CourseViewModel>(course);
        }

        public Task<CourseViewModel> GetCourseByCondition(Expression<Func<UserDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseViewModel>> GetCourses(CourseFilter courseFilter)
        {
            CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.AreaName));
            if (courseArea == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseArea.Fail.NotFoundCourseArea);

            CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.LanguageName));
            if (courseLanguage == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

            CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(courseFilter.CategoryName));
            if (courseCategory == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseCategory.Fail.NotFoundCourseCategory);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(
                predicate: x => x.CourseArea.Name.Equals(courseFilter.AreaName) &&
                                x.CourseLanguage.Name.Equals(courseFilter.LanguageName) &&
                                x.CourseCategory.Name.Equals(courseFilter.CategoryName)
                );

            return _mapper.Map<List<CourseViewModel>>(courses);
        }

        public async Task<List<CourseViewModel>> GetCoursesByArea(string areaName)
        {
            CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(areaName));
            if (courseArea == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseArea.Fail.NotFoundCourseArea);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CourseArea.Name.Equals(areaName));

            return _mapper.Map<List<CourseViewModel>>(courses);
        }

        public async Task<List<CourseViewModel>> GetCoursesByLanguage(string languageName)
        {
            CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(languageName));
            if (courseLanguage == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CourseLanguage.Name.Equals(languageName));

            return _mapper.Map<List<CourseViewModel>>(courses);
        }

        public async Task<List<CourseViewModel>> GetCoursesByCategory(string categoryName)
        {
            CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Name.Equals(categoryName));
            if (courseCategory == null) throw new BadHttpRequestException(MessageConstant.Vi.CourseCategory.Fail.NotFoundCourseCategory);

            ICollection<Course> courses = await _unitOfWork.GetRepository<Course>().GetListAsync(predicate: x => x.CourseCategory.Name.Equals(categoryName));

            return _mapper.Map<List<CourseViewModel>>(courses);
        }

        public Task<bool> Update(UpdateCourseRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
