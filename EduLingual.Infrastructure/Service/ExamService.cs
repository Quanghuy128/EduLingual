using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Dashboard;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Feedback;
using OfficeOpenXml;
using EduLingual.Domain.Pagination;

namespace EduLingual.Infrastructure.Service
{
    public class ExamService : BaseService<Exam>, IExamService
    {
        public ExamService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<Exam> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<PagingResult<Exam>> GetAllExamByCourseId(Guid courseId, int page, int size)
        {
            try
            {
                IPaginate<Exam> exams = await _unitOfWork.GetRepository<Exam>().GetPagingListAsync(predicate: e => e.CourseId.Equals(courseId));

                return SuccessWithPaging<Exam>(
                        exams,
                        page,
                        size,
                        exams.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<Result<Exam>> GetExamById(Guid examId)
        {
            var exam = await _unitOfWork.GetRepository<Exam>().SingleOrDefaultAsync(include: e => e.Include(e => e.Questions).ThenInclude(e => e.Answers));
            if (exam == null) return BadRequest<Exam>(MessageConstant.Vi.Exam.Fail.NotFoundExam);

            return Success(exam);
        }

        public async Task<Result<bool>> ImportExamExcel(Guid teacherId, Guid courseId, IFormFile file)
        {
            string roleName = Enum.GetName(typeof(RoleEnum), RoleEnum.Teacher);
            var teacher = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(include: u => u.Include(u => u.Role), predicate: t => t.Role.RoleName == roleName);
            if (teacher == null) return BadRequest<bool>(MessageConstant.Vi.User.Fail.NotFoundUser);

            var course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(courseId));
            if (course == null) return BadRequest<bool>(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            if (file == null || file.Length == 0) return BadRequest<bool>("No file uploaded!");
            Exam exam = new Exam();
            exam.CenterId = teacherId;
            exam.Title = file.FileName.Split(".")[0];
            exam.CourseId = courseId;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;

                    // Read the content of the Excel file (Example: read the first cell)
                    for (int i = 2; i <= rowCount; i++)
                    {
                        var question = new Question();
                        question.ExamId = exam.Id;
                        question.Content = worksheet.Cells[i, 1].Text;
                        for (int j = 2; j < colCount; j++)
                        {
                            var answer = new Answer
                            {
                                Content = worksheet.Cells[i, j].Text,
                                IsCorrect = j == 2,
                                QuestionId = question.Id
                            };
                            question.Answers.Add(answer);
                        }
                        question.Point = double.Parse(worksheet.Cells[i, colCount].Text);
                        exam.Questions.Add(question);
                    }
                }
            }
            await _unitOfWork.GetRepository<Exam>().InsertAsync(exam);
            var isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.Exam.Fail.CreateExam);
            }

            return Success(isSuccessful);
        }
    }
}
