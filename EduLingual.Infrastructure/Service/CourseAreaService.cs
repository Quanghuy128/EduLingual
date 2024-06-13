using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.User;
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
    public class CourseAreaService : BaseService<CourseAreaService>, ICourseAreaService
    {
        public CourseAreaService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseAreaService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Result<CourseAreaViewModel>> Get(Guid id)
        {
            try
            {
                CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return Success(_mapper.Map<CourseAreaViewModel>(courseArea));
            }
            catch (Exception ex)
            {
                return BadRequest<CourseAreaViewModel>(ex.Message);
            }
        }

        public async Task<Result<List<CourseAreaViewModel>>> GetAll(Expression<Func<CourseArea, bool>>? predicate)
        {
            try
            {
                ICollection<CourseArea> courseAreas = await _unitOfWork.GetRepository<CourseArea>().GetListAsync();
                return Success(_mapper.Map<List<CourseAreaViewModel>>(courseAreas));
            }
            catch (Exception ex)
            {
                return BadRequest<List<CourseAreaViewModel>>(ex.Message);
            }
        }

        public async Task<PagingResult<CourseAreaViewModel>> GetPagination(Expression<Func<CourseArea, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<CourseArea> courseAreas = await _unitOfWork.GetRepository<CourseArea>().GetPagingListAsync(page: page,
                            size: size);

                return SuccessWithPaging<CourseAreaViewModel>(
                        _mapper.Map<IPaginate<CourseAreaViewModel>>(courseAreas),
                        page,
                        size,
                        courseAreas.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<Result<CourseAreaViewModel>> Create(CreateCourseAreaRequest request)
        {
            try
            {
                CourseArea courseArea = new CourseArea()
                {
                    Name = request.Name,
                    Status = request.Status,
                };

                CourseArea result = await _unitOfWork.GetRepository<CourseArea>().InsertAsync(courseArea);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseArea.Fail.CreateCourseArea);
                }

                return Success(_mapper.Map<CourseAreaViewModel>(result));
            }
            catch (Exception ex)
            {
                return Fail<CourseAreaViewModel>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                _unitOfWork.GetRepository<CourseArea>().DeleteAsync(courseArea);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseArea.Fail.DeleteCourseArea);
                }

                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<bool>> Update(Guid id, UpdateCourseAreaRequest request)
        {
            try
            {
                CourseArea courseArea = await _unitOfWork.GetRepository<CourseArea>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                CourseArea newCourseArea = new CourseArea()
                {
                    Id = id,
                    Name = request.Name ?? courseArea.Name,
                    Status = request.Status ?? courseArea.Status
                };

                _unitOfWork.GetRepository<CourseArea>().UpdateAsync(newCourseArea);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseArea.Fail.UpdateCourseArea);
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
