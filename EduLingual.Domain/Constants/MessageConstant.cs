namespace EduLingual.Domain.Constants
{
    public static class MessageConstant
    {
        public static class Vi
        {
            #region User Field
            private const string UserName = "Tên đăng nhập";
            private const string Password = "Mật khẩu";
            private const string FullName = "Tên người dùng";
            private const string Description = "Mô tả";
            private const string UserStatus = "Trạng thái";
            #endregion

            #region Suffix
            private const string RequiredSuffix = " không được bỏ trống !!!";
            #endregion
            public static class User
            {
                public static class Require
                {
                    public const string UserNameRequired = UserName + RequiredSuffix;
                    public const string PasswordRequired = Password + RequiredSuffix;
                    public const string FullNameRequired = FullName + RequiredSuffix;
                    public const string DescriptionRequired = Description + RequiredSuffix;
                    public const string StatusRequired = UserStatus + RequiredSuffix;
                }
            }
            public static class Course
            {
                public static class Require
                {

                }
            }
            public static class Feedback
            {
                public static class Require
                {

                }
            }

        }
    }
}
