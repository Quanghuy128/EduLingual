using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Constants
{
    public static class MessageConstant
    {
        public static class Vi
        {
            #region Template, Suffix, Prefix
            private const string CreateSuccessTemplate = "Tạo mới {0} thành công !!!";
            private const string UpdateSuccessTemplate = "Cập nhật {0} thành công !!!";
            private const string DeleteSuccessTemplate = "Xóa {0} thành công !!!";
            private const string CreateFailTemplate = "Tạo mới {0} thất bại @.@";
            private const string UpdateFailTemplate = "Cập nhật {0} thất bại @.@";
            private const string DeleteFailTemplate = "Xóa {0} thất bại @.@";
            private const string NotFoundTemplate = "{0} không có trong hệ thống";
            private const string InvalidRoleTemplate = "{0} không phải là {1} !!!";

            private const string RequiredSuffix = " không được bỏ trống !!!";
            #endregion
            public static class Auth
            {
                public const string GoogleLoginFail = "Đăng nhập bằng google thất bại!!! Thử lại sau";
                public const string PasswordNotMatched = "Mật khẩu không khớp! Thử lại";
            }
            public static class User
            {
                #region User Field
                private const string UserMessage = "người dùng";
                private const string CenterMessage = "Trung tâm";
                private const string UserName = "Tên đăng nhập";
                private const string Password = "Mật khẩu";
                private const string FullName = "Tên người dùng";
                private const string Email = "email";
                private const string Description = "Mô tả";
                private const string UserStatus = "Trạng thái";
                private const string Role = "Chức vụ";
                #endregion
                public static class Require
                {
                    public const string UserNameRequired = UserName + RequiredSuffix;
                    public const string PasswordRequired = Password + RequiredSuffix;
                    public const string FullNameRequired = FullName + RequiredSuffix;
                    public const string EmailRequired = Email + RequiredSuffix;
                    public const string DescriptionRequired = Description + RequiredSuffix;
                    public const string StatusRequired = UserStatus + RequiredSuffix;
                    public const string RoleRequired = Role + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateUser = String.Format(CreateSuccessTemplate, UserMessage);
                    public static string UpdateUser = String.Format(UpdateSuccessTemplate, UserMessage);
                    public static string DeleteUser = String.Format(DeleteSuccessTemplate, UserMessage);
                }
                public static class Fail
                {
                    public static string CreateUser = String.Format(CreateFailTemplate, UserMessage);
                    public static string UpdateUser = String.Format(UpdateFailTemplate, UserMessage);
                    public static string DeleteUser = String.Format(DeleteFailTemplate, UserMessage);
                    public static string NotFoundCenter = String.Format(NotFoundTemplate, CenterMessage);
                    public static string NotFoundUser = String.Format(NotFoundTemplate, UserMessage);
                }
            }
            public static class Course
            {
                #region Course Field
                private const string CenterMessage = "Trung tâm";
                private const string CourseMessage = "Khóa học";
                private const string Title = "Tiêu đề";
                private const string Description = "Mô tả";
                private const string Duration = "Thời lượng";
                private const string TutionFee = "Học phí";
                private const string Center = "Trung tâm";
                private const string Area = "Khu vực";
                private const string Language = "Ngôn ngữ";
                private const string Category = "Loại khóa học";
                private const string CourseStatus = "Trạng thái";
                #endregion
                public static class Require
                {
                    public const string TitleRequired = Title + RequiredSuffix;
                    public const string DescriptionRequired = Description + RequiredSuffix;
                    public const string DurationRequired = Duration + RequiredSuffix;
                    public const string TutionFeeRequired = TutionFee + RequiredSuffix;
                    public const string CenterRequired = Center + RequiredSuffix;
                    public const string AreaRequired = Area + RequiredSuffix;
                    public const string LanguageRequired = Language + RequiredSuffix;
                    public const string CategoryRequired = Category + RequiredSuffix;
                    public const string StatusRequired = CourseStatus + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateCourse = String.Format(CreateSuccessTemplate, CourseMessage);
                    public static string UpdateCourse = String.Format(UpdateSuccessTemplate, CourseMessage);
                    public static string DeleteCourse = String.Format(DeleteSuccessTemplate, CourseMessage);
                }
                public static class Fail
                {
                    public static string CreateCourse = String.Format(CreateFailTemplate, CourseMessage);
                    public static string UpdateCourse = String.Format(UpdateFailTemplate, CourseMessage);
                    public static string DeleteCourse = String.Format(DeleteFailTemplate, CourseMessage);
                    public static string NotFoundCenter = String.Format(NotFoundTemplate, CenterMessage);
                    public static string NotFoundCourse = String.Format(NotFoundTemplate, CourseMessage);
                }
            }
            public static class CourseArea
            {
                #region CourseArea Field
                private const string CourseAreaMessage = "Khu vực";
                private const string Name = "Tên khu vực";
                private const string CourseAreaStatus = "Trạng thái";
                #endregion
                public static class Require
                {
                    public const string NameRequired = Name + RequiredSuffix;
                    public const string StatusRequired = CourseAreaStatus + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateCourseArea = String.Format(CreateSuccessTemplate, CourseAreaMessage);
                    public static string UpdateCourseArea = String.Format(UpdateSuccessTemplate, CourseAreaMessage);
                    public static string DeleteCourseArea = String.Format(DeleteSuccessTemplate, CourseAreaMessage);
                }
                public static class Fail
                {
                    public static string CreateCourseArea = String.Format(CreateFailTemplate, CourseAreaMessage);
                    public static string UpdateCourseArea = String.Format(UpdateFailTemplate, CourseAreaMessage);
                    public static string DeleteCourseArea = String.Format(DeleteFailTemplate, CourseAreaMessage);
                    public static string NotFoundCourseArea = String.Format(NotFoundTemplate, CourseAreaMessage);
                }
            }
            public static class CourseLanguage
            {
                #region CourseLanguage Field
                private const string CourseLanguageMessage = "Ngôn ngữ";
                private const string Name = "Tên ngôn ngữ";
                private const string CourseLanguageStatus = "Trạng thái";
                #endregion
                public static class Require
                {
                    public const string NameRequired = Name + RequiredSuffix;
                    public const string StatusRequired = CourseLanguageStatus + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateCourseLanguage = String.Format(CreateSuccessTemplate, CourseLanguageMessage);
                    public static string UpdateCourseLanguage = String.Format(UpdateSuccessTemplate, CourseLanguageMessage);
                    public static string DeleteCourseLanguage = String.Format(DeleteSuccessTemplate, CourseLanguageMessage);
                }
                public static class Fail
                {
                    public static string CreateCourseLanguage = String.Format(CreateFailTemplate, CourseLanguageMessage);
                    public static string UpdateCourseLanguage = String.Format(UpdateFailTemplate, CourseLanguageMessage);
                    public static string DeleteCourseLanguage = String.Format(DeleteFailTemplate, CourseLanguageMessage);
                    public static string NotFoundCourseLanguage = String.Format(NotFoundTemplate, CourseLanguageMessage);
                }
            }
            public static class CourseCategory
            {
                #region CourseCategory Field
                private const string CourseCategoryMessage = "Loại khóa học";
                private const string Name = "Tên loại khóa học";
                private const string Language = "Ngôn ngữ";
                private const string CourseCategoryStatus = "Trạng thái";
                #endregion
                public static class Require
                {
                    public const string NameRequired = Name + RequiredSuffix;
                    public const string LanguageRequired = Language + RequiredSuffix;
                    public const string StatusRequired = CourseCategoryStatus + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateCourseCategory = String.Format(CreateSuccessTemplate, CourseCategoryMessage);
                    public static string UpdateCourseCategory = String.Format(UpdateSuccessTemplate, CourseCategoryMessage);
                    public static string DeleteCourseCategory = String.Format(DeleteSuccessTemplate, CourseCategoryMessage);
                }
                public static class Fail
                {
                    public static string CreateCourseCategory = String.Format(CreateFailTemplate, CourseCategoryMessage);
                    public static string UpdateCourseCategory = String.Format(UpdateFailTemplate, CourseCategoryMessage);
                    public static string DeleteCourseCategory = String.Format(DeleteFailTemplate, CourseCategoryMessage);
                    public static string NotFoundCourseCategory = String.Format(NotFoundTemplate, CourseCategoryMessage);
                }
            }
            public static class Feedback
            {
                #region Feedback field
                private const string FeedbackMessage = "bình luận";
                private const string DescriptionMessage = "Mô tả";
                private const string FeedbackStatus = "Trạng thái";
                private const string UserMessage = "người dùng";
                #endregion
                public static class Require
                {
                    public const string DescriptionRequired = DescriptionMessage + RequiredSuffix;
                    public const string StatusRequired = FeedbackStatus + RequiredSuffix;
                    public const string UserRequired = UserMessage + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreateFeedback = String.Format(CreateSuccessTemplate, FeedbackMessage);
                    public static string UpdateFeedback = String.Format(UpdateSuccessTemplate, FeedbackMessage);
                    public static string DeleteFeedback = String.Format(DeleteSuccessTemplate, FeedbackMessage);
                }
                public static class Fail
                {
                    public static string CreateFeedback = String.Format(CreateFailTemplate, FeedbackMessage);
                    public static string UpdateFeedback = String.Format(UpdateFailTemplate, FeedbackMessage);
                    public static string DeleteFeedback = String.Format(DeleteFailTemplate, FeedbackMessage);
                    public static string NotFoundFeedback = String.Format(NotFoundTemplate, FeedbackMessage);
                }
            }
            public static class UserCourse
            {
                #region UserCourse field 
                private const string CourseMessage = "khóa học";
                private const string UserMessage = "người dùng";
                private const string StudentRoleMessage = "học sinh";
                #endregion
                public static class Require
                {
                    public const string UserIdRequired = UserMessage + RequiredSuffix;
                    public const string CourseIdRequired = CourseMessage + RequiredSuffix;
                }
                public static class Fail
                {
                    public static string UserNotStudentRole = String.Format(InvalidRoleTemplate, UserMessage, StudentRoleMessage);
                    public static string JoinCourseFail = $"{UserMessage} tham gia {CourseMessage} thất bại";
                }
            }
            public static class Payment
            {
                #region Payment Field
                private const string PaymentMessage = "Thanh toán";
                private const string PaymentMethod = "Phương thức thanh toán";
                private const string Fee = "Học phí";
                private const string Course = "Khóa học";
                private const string User = "Người học";
                #endregion
                public static class Require
                {
                    public const string PaymentMethodRequired = PaymentMethod + RequiredSuffix;
                    public const string FeeRequired = Fee + RequiredSuffix;
                    public const string CourseRequired = Course + RequiredSuffix;
                    public const string UserRequired = User + RequiredSuffix;
                }
                public static class Success
                {
                    public static string CreatePayment = String.Format(CreateSuccessTemplate, PaymentMessage);
                    public static string UpdatePayment = String.Format(UpdateSuccessTemplate, PaymentMessage);
                    public static string DeleteCPayment = String.Format(DeleteSuccessTemplate, PaymentMessage);
                }
                public static class Fail
                {
                    public static string CreatePayment = String.Format(CreateFailTemplate, PaymentMessage);
                    public static string UpdatePayment = String.Format(UpdateFailTemplate, PaymentMessage);
                    public static string DeletePayment = String.Format(DeleteFailTemplate, PaymentMessage);
                    public static string NotFoundPayment = String.Format(NotFoundTemplate, PaymentMessage);
                }
            }

        }
    }
}
