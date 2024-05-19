﻿using EduLingual.Application.Service;
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
    public class CourseLanguageService : BaseService<CourseLanguageService>, ICourseLanguageService
    {
        public CourseLanguageService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseLanguageService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
    }
}
