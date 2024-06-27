using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Exam;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Linq;

namespace EduLingual.Infrastructure.Service
{
    public class ExamService : BaseService<Exam>, IExamService
    {
        public ExamService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<Exam> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<PagingResult<ExamDto>> GetAllExamByCourseId(Guid courseId, string? examName, int page, int size)
        {
            try
            {
                IPaginate<ExamDto> exams = await _unitOfWork.GetRepository<Exam>().GetPagingListAsync(
                        selector: x => new ExamDto(x.Id, x.CreatedAt, x.IsDeleted, x.Title, x.TotalQuestion, _mapper.Map<UserDto>(x.Center)),
                        predicate: e => String.IsNullOrEmpty(examName) ? e.CourseId.Equals(courseId) && e.IsDeleted == false : e.CourseId.Equals(courseId) && e.Title.ToLower().Contains(examName.ToLower().Trim()) && e.IsDeleted == false,
                        include: x => x.Include(x => x.Center)
                    );

                return SuccessWithPaging<ExamDto>(
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

            var title = file.FileName.Split(".")[0];
            var examFromDb = await _unitOfWork.GetRepository<Exam>().SingleOrDefaultAsync(predicate: x => x.Title.Equals(title));
            if (examFromDb != null) return BadRequest<bool>("Tên bài kiểm tra đã tồn tại !!!");

            exam.Title = title;
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

            exam.TotalQuestion = exam.Questions.Count();

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

            UserExam userExam = new UserExam()
            {
                UserId = user.Id,
                ExamId = exam.Id,         
            };

            var totalQuestionRight = 0;
            var totalQuestionWrong = 0;
            foreach (var a in resultExamDto.Results)
            {

                var answer = await _unitOfWork.GetRepository<Answer>().SingleOrDefaultAsync(
                        include: aw => aw.Include(a => a.Question), 
                        predicate: aw => aw.Id.Equals(a) && aw.IsCorrect
                    );
                if (answer != null && answer.IsCorrect == true)
                {
                    totalQuestionRight++;
                    score += answer.Question.Point;
                }
                else
                {
                    totalQuestionWrong++;
                    continue;
                }
            }
            //var finalScore = score * 10 / totalScore;
            userExam.TotalQuestionRight = totalQuestionRight;
            userExam.TotalQuestionWrong = totalQuestionWrong;
            userExam.Score = score;

            await _unitOfWork.GetRepository<UserExam>().InsertAsync(userExam);
            var isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.UserExam.Fail.CreateUserExam);
            }

            return Success(isSuccessful);
        }

        public async Task<PagingResult<GetScoreResponse>> GetScoreExam(GetScoreDto getScoreDto, int page, int size)
        {
            try
            {
                IPaginate<GetScoreResponse> userExams = await _unitOfWork.GetRepository<UserExam>().GetPagingListAsync(
                        selector: x => new GetScoreResponse(x.Id, x.CreatedAt, x.Score, x.TotalQuestionRight, x.TotalQuestionWrong, _mapper.Map<ExamDto>(x.Exam), _mapper.Map<UserDto>(x.Exam.Course.Center)),
                        predicate: ue => String.IsNullOrEmpty(getScoreDto.ExamName) 
                                         ? ue.UserId.Equals(getScoreDto.UserId) && ue.Exam.CourseId.Equals(getScoreDto.CourseId)
                                         : ue.UserId.Equals(getScoreDto.UserId) && ue.Exam.CourseId.Equals(getScoreDto.CourseId) && ue.Exam.Title.ToLower().Trim().Contains(getScoreDto.ExamName.ToLower().Trim()),
                        include: x => x.Include(x => x.Exam).ThenInclude(x => x.Course).ThenInclude(x => x.Center),
                        orderBy: x => x.OrderByDescending(x => x.CreatedAt)
                    );

                return SuccessWithPaging<GetScoreResponse>(
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

                if (exam == null)
                {
                    throw new Exception(MessageConstant.Vi.Exam.Fail.DeleteExam);
                }

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
