namespace EduLingual.Domain.Constants
{
    public static class ApiEndPointConstant
    {
        static ApiEndPointConstant()
        {
        }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public const string ByIdRoute = "/{id}";
        public static class Authentication
        {
            /// <summary>"api/v1/login"</summary>
            public const string LoginEndPoint = ApiEndpoint + "/login";
        }
        public static class User
        {
            /// <summary>"api/v1/users"</summary>
            public const string UsersEndpoint = ApiEndpoint + "/users";
            /// <summary>"api/v1/users/{id}"</summary>
            public const string UserEndpoint = UsersEndpoint + ByIdRoute;
            public const string CoursesByCenterEndpoint = UsersEndpoint + "/{id}/khoa-hoc";
        }
        public static class Course
        {
            public const string CoursesEndpoint = ApiEndpoint + "/khoa-hoc";
            public const string CourseEndpoint = CoursesEndpoint + ByIdRoute;
            public const string CoursesPaginationEndpoint = ApiEndpoint + "/khoa-hoc-1";
        }
        public static class CourseArea
        {
            public const string CourseAreasEndpoint = ApiEndpoint + "/khu-vuc";
            public const string CourseAreaEndpoint = CourseAreasEndpoint + ByIdRoute;
        }
        public static class CourseLanguage
        {
            public const string CourseLanguagesEndpoint = ApiEndpoint + "/ngon-ngu";
            public const string CourseLanguageEndpoint = CourseLanguagesEndpoint + ByIdRoute;
        }
        public static class CourseCategory
        {
            public const string CourseCategoriesEndpoint = ApiEndpoint + "/loai-khoa-hoc";
            public const string CourseCategoryEndpoint = CourseCategoriesEndpoint + ByIdRoute;
        }
    }
}
