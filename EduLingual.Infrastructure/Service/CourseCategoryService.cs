using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseCategory;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public class CourseCategoryService : BaseService<CourseCategoryService>, ICourseCategoryService
    {
        public CourseCategoryService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseCategoryService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<Result<CourseCategoryViewModel>> Get(Guid id)
        {
            try
            {
                CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return Success(_mapper.Map<CourseCategoryViewModel>(courseCategory));
            }
            catch (Exception ex)
            {
                return BadRequest<CourseCategoryViewModel>(ex.Message);
            }
        }

        public async Task<Result<List<CourseCategoryViewModel>>> GetAll(Expression<Func<CourseCategory, bool>>? predicate)
        {
            try
            {
                ICollection<CourseCategory> courseCategories = await _unitOfWork.GetRepository<CourseCategory>().GetListAsync();
                return Success(_mapper.Map<List<CourseCategoryViewModel>>(courseCategories));
            }
            catch (Exception ex)
            {
                return BadRequest<List<CourseCategoryViewModel>>(ex.Message);
            }
        }

        public async Task<PagingResult<CourseCategoryViewModel>> GetPagination(Expression<Func<CourseCategory, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<CourseCategory> courseCategories = await _unitOfWork.GetRepository<CourseCategory>().GetPagingListAsync();

                return SuccessWithPaging<CourseCategoryViewModel>(
                        _mapper.Map<IPaginate<CourseCategoryViewModel>>(courseCategories),
                        page,
                        size,
                        courseCategories.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<Result<CourseCategoryViewModel>> Create(CreateCourseCategoryRequest request)
        {
            try
            {
                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.LanguageId));
                if (courseLanguage == null) throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

                CourseCategory courseCategory = new CourseCategory()
                {
                    Name = request.Name,
                    LanguageId = request.LanguageId,
                    Status = request.Status,
                };

                CourseCategory result = await _unitOfWork.GetRepository<CourseCategory>().InsertAsync(courseCategory);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseCategory.Fail.CreateCourseCategory);
                }

                return Success(_mapper.Map<CourseCategoryViewModel>(result));
            }
            catch (Exception ex)
            {
                return Fail<CourseCategoryViewModel>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                _unitOfWork.GetRepository<CourseCategory>().DeleteAsync(courseCategory);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseCategory.Fail.DeleteCourseCategory);
                }

                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<bool>> Update(Guid id, UpdateCourseCategoryRequest request)
        {
            try
            {
                CourseCategory courseCategory = await _unitOfWork.GetRepository<CourseCategory>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.LanguageId));
                if (courseLanguage == null) throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

                CourseCategory newCourseCategory = new CourseCategory()
                {
                    Id = id,
                    Name = request.Name ?? courseCategory.Name,
                    LanguageId = request.LanguageId ?? courseCategory.LanguageId,
                    Status = request.Status ?? courseCategory.Status
                };

                _unitOfWork.GetRepository<CourseCategory>().UpdateAsync(newCourseCategory);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseCategory.Fail.UpdateCourseCategory);
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
