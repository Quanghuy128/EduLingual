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

            private const string RequiredSuffix = " không được bỏ trống !!!";
            #endregion
            public static class User
            {
                #region User Field
                private const string UserMessage = "người dùng";
                private const string CenterMessage = "Trung tâm";
                private const string UserName = "Tên đăng nhập";
                private const string Password = "Mật khẩu";
                private const string FullName = "Tên người dùng";
                private const string Description = "Mô tả";
                private const string UserStatus = "Trạng thái";
                private const string Role = "Chức vụ";
                #endregion
                public static class Require
                {
                    public const string UserNameRequired = UserName + RequiredSuffix;
                    public const string PasswordRequired = Password + RequiredSuffix;
                    public const string FullNameRequired = FullName + RequiredSuffix;
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
                }
            }
            public static class Course
            {
                #region Course Field
                private const string CourseMessage = "Course";
                #endregion
                public static class Require
                {

                }
                public static class Success
                {

                }
                public static class Fail
                {
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
                public static class Require
                {

                }
                public static class Success
                {

                }
                public static class Fail
                {

                }
            }

        }
    }
}
