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
            public const string CoursesEndpoint = ApiEndpoint + "/courses";
            public const string CourseEndpoint = CoursesEndpoint + ByIdRoute;
        }
    }
}
