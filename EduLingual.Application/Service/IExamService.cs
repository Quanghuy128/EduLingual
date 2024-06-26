﻿using EduLingual.Domain.Common;
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
    Task<PagingResult<ExamDto>> GetAllExamByCourseId(Guid courseId, string? examName, int page, int size);
    Task<Result<bool>> GenerateScore(ResultExamDto resultExamDto);
    Task<PagingResult<GetScoreResponse>> GetScoreExam(GetScoreDto getScoreDto, int page, int size);
    Task<Result<bool>> Delete(Guid id);
}
