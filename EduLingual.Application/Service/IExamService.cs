using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Exam;
using EduLingual.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service;

public interface IExamService
{
    Task<Result<bool>> ImportExamExcel(Guid teacherId, Guid courseId, IFormFile file);
    Task<Result<Exam>> GetExamById(Guid examId);
    Task<PagingResult<Exam>> GetAllExamByCourseId(Guid courseId, int page, int size);
    Task<Result<bool>> GenerateScore(ResultExamDto resultExamDto);
}
