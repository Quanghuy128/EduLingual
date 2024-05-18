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
    }
}
