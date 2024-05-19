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
        public static class User
        {
            public const string UsersEndpoint = ApiEndpoint + "/users";
            public const string UserEndpoint = UsersEndpoint + ByIdRoute;
        }
        public static class Course
        {
            public const string CoursesEndpoint = ApiEndpoint + "/khoa-hoc";
            public const string CourseEndpoint = CoursesEndpoint + ByIdRoute;
            public const string CoursesByLanguageEndpoint = CoursesEndpoint + "-theo-ngon-ngu";
            public const string CoursesByAreaEndpoint = CoursesEndpoint + "-theo-khu-vuc";
            public const string CoursesByCategoryEndpoint = CoursesEndpoint + "-theo-danh-muc";
        }
    }
}
