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

            private const string RequiredSuffix = " không được bỏ trống !!!";
            #endregion
            public static class User
            {
                #region User Field
                private const string UserMessage = "người dùng";
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
                }
            }
            public static class Course
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
