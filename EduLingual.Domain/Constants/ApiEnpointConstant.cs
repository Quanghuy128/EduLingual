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
        public static class Media { 
            public const string UploadEndpoint = ApiEndpoint+ "/upload";
            public const string DownloadEndpoint = ApiEndpoint+ "/download";
        }
        public static class Authentication
        {
            /// <summary>"api/v1/login"</summary>
            public const string LoginEndPoint = ApiEndpoint + "/login";
            /// <summary>"api/v1/google/login"</summary>
            public const string GoogleLoginEndPoint = ApiEndpoint + "/google/login";
            // <summary>"api/v1/google/login"</summary>
            public const string RegisterEndPoint = ApiEndpoint + "/register";
            // <summary>"api/v1/google/login"</summary>
            public const string ForgetPasswordEndPoint = ApiEndpoint + "/forget-password";
        }
        public static class User
        {
            /// <summary>"api/v1/users"</summary>
            public const string UsersEndpoint = ApiEndpoint + "/users";
            public const string StudentsEnpoint = ApiEndpoint + "/nguoi-hoc";
            public const string CentersEndpoint = ApiEndpoint + "/trung-tam";
            /// <summary>"api/v1/users/{id}"</summary>
            public const string UserEndpoint = UsersEndpoint + ByIdRoute;
            public const string CoursesByCenterEndpoint = CentersEndpoint + "/{id}/khoa-hoc";
            public const string CoursesByUserEndpoint = StudentsEnpoint + "/{id}/khoa-hoc";
        }
        public static class Course
        {
            public const string CoursesEndpoint = ApiEndpoint + "/khoa-hoc";
            public const string CoursesPaginationEndpoint = ApiEndpoint + "/khoa-hoc-admin";
            public const string CourseEndpoint = CoursesEndpoint + ByIdRoute;
            public const string HighlightedCoursesEndpoint = CoursesEndpoint + "-noi-bat";
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
            public const string CategoriesByLanguageEndpoint = CourseLanguagesEndpoint + "/{id}/loai-khoa-hoc";
        }
        public static class CourseCategory
        {
            public const string CourseCategoriesEndpoint = ApiEndpoint + "/loai-khoa-hoc";
            public const string CourseCategoryEndpoint = CourseCategoriesEndpoint + ByIdRoute;
        }
        public static class Feedback
        {
            public const string FeedbacksEndpoint = ApiEndpoint + "/binh-luan";
            public const string FeedbackEndpoint = FeedbacksEndpoint + ByIdRoute;
            public const string FeedbacksPaginationEndpoint = ApiEndpoint + "/binh-luan-paging";
        }
        public static class Dashboard
        {
            public const string DashdoardEndpoint = ApiEndpoint + "/dashboard";
            public const string DashboardFinance = DashdoardEndpoint + "/finance";
            public const string DashboardTeacher = DashdoardEndpoint + "/teacher";
            public const string DashboardUser = DashdoardEndpoint + "/user";
            public const string DashboardExam = DashdoardEndpoint + "/exam";

        }
        public static class Exam
        {
            public const string ExamEndpoint = ApiEndpoint + "/exam";
            public const string ExamCreate = ExamEndpoint + "/upload";
            public const string ExamId = ExamEndpoint + ByIdRoute;
            public const string ExamsByCourseId = ExamEndpoint + "/courseId" + ByIdRoute;
            public const string ExamCreateResult = ExamEndpoint + "/result";
            public const string ExamResult = ExamEndpoint + "/get-score";
        }
        public static class UserCourse
        {
            public const string CourseUsersEndpoint = ApiEndpoint + "/UserCourse";
            public const string CourseUserEndpointJoin = CourseUsersEndpoint + "/join";
        }
        public static class PayOs
        {
            public const string PayOsEndpoint = ApiEndpoint + "/PayOs";
        }
        public static class Payment
        {
            public const string PaymentsEndpoint = ApiEndpoint + "/thanh-toan";
            public const string PaymentEndpoint = PaymentsEndpoint + ByIdRoute;
        }
    }
}
