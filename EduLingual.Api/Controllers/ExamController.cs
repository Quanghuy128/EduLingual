﻿using EduLingual.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EduLingual.Application.Service;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Common;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : BaseController<ExamController>
    {
        IExamService _examService;

        public ExamController(ILogger<ExamController> logger, IExamService examService) : base(logger)
        {
            _examService = examService;
        }
        [HttpPost(ApiEndPointConstant.Exam.ExamCreate)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportExamExcel([FromQuery] Guid teacherId,[FromQuery] Guid courseId, IFormFile file)
        {
            var result = await _examService.ImportExamExcel(teacherId, courseId, file);
            return Ok(result);
        }
        [HttpGet(ApiEndPointConstant.Exam.ExamId)]
        [ProducesResponseType(typeof(Result<Exam>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExamById([FromRoute] Guid id)
        {
            var result = await _examService.GetExamById(id);
            return Ok(result);
        }
        [HttpGet(ApiEndPointConstant.Exam.ExamsByCourseId)]
        [ProducesResponseType(typeof(PagingResult<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExamsByCourseId([FromRoute] Guid id, [FromQuery] int page, [FromQuery] int size)
        {
            var result = await _examService.GetAllExamByCourseId(id, page, size);
            return Ok(result);
        }
    }
}
