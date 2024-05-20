using EduLingual.Api.Utils;
using EduLingual.Domain.Entities;
using EduLingual.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureSeedingData
    {
        public static async Task DbInitializer(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await Seeding.InitializeAsync(dbContext);
        }
        public static async Task ApplyMigration(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
    public static class Seeding
    {
        public static async Task InitializeAsync(ApplicationDbContext dbContext)
        {
            await dbContext.SeedRoles();
            await dbContext.SeedUsers();
            await dbContext.SeedAreas();
            await dbContext.SeedCategories();
            await dbContext.SeedLanguages();
            await dbContext.SeedCourses();
        }
        
        public static async Task SeedRoles(this ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            await context.Roles.AddRangeAsync(
                new List<Role>{
                    new Role
                    {
                        Id = new Guid("77f2299e-3743-4afe-8e87-978f6afd831a"),
                        RoleName = "User"
                    },
                    new Role
                    {
                        Id = new Guid("f475a5e7-f15f-4100-b7b4-90f9dbc581c1"),
                        RoleName = "Teacher"
                    },
                    new Role
                    {
                        Id = new Guid("453cb74c-95c8-43c7-bcc5-82b5a79603e0"),
                        RoleName = "Admin"
                    }
                }
            );
        }

        public static async Task SeedAreas(this ApplicationDbContext context)
        {
            if (context.CourseAreas.Any())
            {
                return;
            }
            await context.CourseAreas.AddRangeAsync(
                new List<CourseArea>{
                    new CourseArea
                    {
                        Name = "Hồ Chí Minh",
                    },
                    new CourseArea
                    {
                        Name = "Đà Nẵng",
                    },
                    new CourseArea
                   {
                        Name = "Hà Nội",
                    }
                }
            );

            await context.SaveChangesAsync();
        }

        public static async Task SeedCategories(this ApplicationDbContext context)
        {
            if (context.CoursesCategories.Any())
            {
                return;
            }
            await context.CoursesCategories.AddRangeAsync(
                new List<CourseCategory>{
                    new CourseCategory
                    {
                        Name = "Giao tiếp",
                        LanguageId = Guid.Parse("c951f636-74e7-437e-aaf8-9ba0d3d44991")
                    },
                    new CourseCategory
                    {
                        Name = "TOEIC",
                        LanguageId = Guid.Parse("c951f636-74e7-437e-aaf8-9ba0d3d44991")
                    },
                    new CourseCategory
                   {
                        Name = "IELTS",
                        LanguageId = Guid.Parse("c951f636-74e7-437e-aaf8-9ba0d3d44991")
                    }
                }
            );

            await context.SaveChangesAsync();
        }

        public static async Task SeedLanguages(this ApplicationDbContext context)
        {
            if (context.CoursesLanguages.Any())
            {
                return;
            }
            await context.CoursesLanguages.AddRangeAsync(
                new List<CourseLanguage>{
                    new CourseLanguage
                    {
                        Name = "Tiếng Anh",
                    },
                    new CourseLanguage
                    {
                        Name = "Tiếng Trung",
                    },
                    new CourseLanguage
                   {
                        Name = "Tiếng Nhật",
                    }
                }
            );

            await context.SaveChangesAsync();
        }

        public static async Task SeedUsers(this ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            List<User> users = FileHelper.LoadJson<List<User>>("./MockData/", "user.json");
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCourses(this ApplicationDbContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }
            List<Course> courses = FileHelper.LoadJson<List<Course>>("./MockData/", "course.json");
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}
