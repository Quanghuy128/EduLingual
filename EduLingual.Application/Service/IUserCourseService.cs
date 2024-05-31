using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.UserCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service;

public interface IUserCourseService
{
    Task<Result<bool>> UserJoinCourseAsync(UserCourseRequest request);
}
