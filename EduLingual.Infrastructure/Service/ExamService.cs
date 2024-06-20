using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Exam;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

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
            var exam = await _unitOfWork.GetRepository<Exam>().SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(examId),
                include: e => e.Include(e => e.Questions).ThenInclude(e => e.Answers));
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
                    var colCount = 6;

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
        public async Task<Result<bool>> GenerateScore(ResultExamDto resultExamDto)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Id.Equals(resultExamDto.UserId));
            if (user == null) return BadRequest<bool>(MessageConstant.Vi.User.Fail.NotFoundUser);

            var exam = await _unitOfWork.GetRepository<Exam>().SingleOrDefaultAsync(predicate: e => e.Id.Equals(resultExamDto.ExamId), include: e => e.Include(e => e.Questions));
            if (exam == null) return BadRequest<bool>(MessageConstant.Vi.Exam.Fail.NotFoundExam);

            var userCourse = await _unitOfWork.GetRepository<UserCourse>().SingleOrDefaultAsync(predicate: uc => uc.UserId.Equals(user.Id) && uc.CourseId.Equals(exam.CourseId));
            if (userCourse == null) return BadRequest<bool>(MessageConstant.Vi.UserCourse.Fail.UserNotInCourse);

            var totalScore = exam.Questions.Sum(e => e.Point);
            double score = 0;
            foreach (var a in resultExamDto.Results)
            {

                var answer = await _unitOfWork.GetRepository<Answer>().SingleOrDefaultAsync(include: aw => aw.Include(a => a.Question), predicate: aw => aw.Id.Equals(a) && aw.IsCorrect);
                if (answer != null && answer.IsCorrect == true)
                {
                    score += answer.Question.Point;
                }
                else
                {
                    continue;
                }
            }
            //var finalScore = score * 10 / totalScore;
            UserExam userExam = new UserExam()
            {
                UserId = user.Id,
                ExamId = exam.Id,
                Score = score
            };
            await _unitOfWork.GetRepository<UserExam>().InsertAsync(userExam);
            var isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.UserExam.Fail.CreateUserExam);
            }

            return Success(isSuccessful);
        }

        public async Task<PagingResult<UserExam>> GetScoreExam(GetScoreDto getScoreDto, int page, int size)
        {
            try
            {
                IPaginate<UserExam> userExams = await _unitOfWork.GetRepository<UserExam>().GetPagingListAsync(predicate: ue => ue.ExamId.Equals(getScoreDto.ExamId) && ue.UserId.Equals(getScoreDto.UserId));

                return SuccessWithPaging<UserExam>(
                        userExams,
                        page,
                        size,
                        userExams.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                Exam exam = await _unitOfWork.GetRepository<Exam>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                exam.IsDeleted = true;

                _unitOfWork.GetRepository<Exam>().UpdateAsync(exam);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Exam.Fail.DeleteExam);
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
