using EduLingual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduLingual.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }
        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLanguage> CoursesLanguages { get; set;}
        public DbSet<CourseCategory> CoursesCategories { get; set;}
        public DbSet<CourseArea> CourseAreas { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("server=127.0.0.1;port=5432;database=edulingual;uid=postgres;password=root;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.HasDefaultSchema("edl");

            #region Entity Relation
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany(d => d.Users)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Feedback>()
                .HasOne(p => p.User)
                .WithMany(d => d.Feedbacks)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Course>()
                .HasMany(p => p.Users)
                .WithMany(d => d.Courses)
                .UsingEntity<UserCourse>(
                    l => l.HasOne<User>(e => e.User).WithMany(e => e.UserCourses),
                    r => r.HasOne<Course>(e => e.Course).WithMany(e => e.UserCourses)
                );

            modelBuilder.Entity<Course>()
                .HasMany(p => p.Feedbacks)
                .WithMany(d => d.Courses)
                .UsingEntity<CourseFeedback>(
                    l => l.HasOne<Feedback>(e => e.Feedback).WithMany(e => e.CourseFeedbacks),
                    r => r.HasOne<Course>(e => e.Course).WithMany(e => e.CourseFeedbacks)
                );

            modelBuilder.Entity<Course>()
                .HasOne(p => p.CourseArea)
                .WithMany(d => d.Courses)
                .HasForeignKey(p => p.CourseAreaId);

            modelBuilder.Entity<Course>()
               .HasOne(p => p.CourseCategory)
               .WithMany(d => d.Courses)
               .HasForeignKey(p => p.CourseCategoryId);

            modelBuilder.Entity<Course>()
               .HasOne(p => p.CourseLanguage)
               .WithMany(d => d.Courses)
               .HasForeignKey(p => p.CourseLanguageId);

            modelBuilder.Entity<Course>()
               .HasOne(p => p.Center)
               .WithMany(d => d.OwnCourses)
               .HasForeignKey(p => p.CenterId);

            modelBuilder.Entity<CourseCategory>()
               .HasOne(p => p.CourseLanguage)
               .WithMany(d => d.CourseCategories)
               .HasForeignKey(p => p.LanguageId);
            #endregion
        }
    }
}
