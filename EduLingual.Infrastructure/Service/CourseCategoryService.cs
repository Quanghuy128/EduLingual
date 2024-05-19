using EduLingual.Application.Service;
using EduLingual.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
