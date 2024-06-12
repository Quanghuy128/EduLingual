﻿// <auto-generated />
using System;
using EduLingual.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EduLingual.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("edl")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EduLingual.Domain.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CenterId")
                        .HasColumnType("uuid")
                        .HasColumnName("center_id");

                    b.Property<Guid>("CourseAreaId")
                        .HasColumnType("uuid")
                        .HasColumnName("area_id");

                    b.Property<Guid>("CourseCategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<Guid>("CourseLanguageId")
                        .HasColumnType("uuid")
                        .HasColumnName("language_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsHighlighted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_highlighted");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<double>("Tuitionfee")
                        .HasColumnType("double precision")
                        .HasColumnName("tuition_fee");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("CenterId");

                    b.HasIndex("CourseAreaId");

                    b.HasIndex("CourseCategoryId");

                    b.HasIndex("CourseLanguageId");

                    b.ToTable("course", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("course_area", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid")
                        .HasColumnName("language_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("course_category", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseFeedback", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<Guid>("FeedbackId")
                        .HasColumnType("uuid")
                        .HasColumnName("feedback_id");

                    b.HasKey("CourseId", "FeedbackId");

                    b.HasIndex("FeedbackId");

                    b.ToTable("course_feedback", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("course_language", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CenterId")
                        .HasColumnType("uuid")
                        .HasColumnName("center_id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("CenterId");

                    b.HasIndex("CourseId");

                    b.ToTable("Exams", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<byte>("FeedbackStatus")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("feedback", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<double>("Fee")
                        .HasColumnType("double precision")
                        .HasColumnName("fee");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("method");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("payment", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid")
                        .HasColumnName("exam_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<double>("Point")
                        .HasColumnType("double precision")
                        .HasColumnName("point");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("role", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fullname");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("user", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.UserExam", b =>
                {
                    b.Property<Guid>("ExamId")
                        .HasColumnType("uuid")
                        .HasColumnName("exam_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.HasKey("ExamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("user_exam", "edl");
                });

            modelBuilder.Entity("EduLingual.Infrastructure.UserCourse", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("user_course", "edl");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Answer", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Course", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.User", "Center")
                        .WithMany("OwnCourses")
                        .HasForeignKey("CenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.CourseArea", "CourseArea")
                        .WithMany("Courses")
                        .HasForeignKey("CourseAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.CourseCategory", "CourseCategory")
                        .WithMany("Courses")
                        .HasForeignKey("CourseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.CourseLanguage", "CourseLanguage")
                        .WithMany("Courses")
                        .HasForeignKey("CourseLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Center");

                    b.Navigation("CourseArea");

                    b.Navigation("CourseCategory");

                    b.Navigation("CourseLanguage");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseCategory", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.CourseLanguage", "CourseLanguage")
                        .WithMany("CourseCategories")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseLanguage");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseFeedback", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Course", "Course")
                        .WithMany("CourseFeedbacks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.Feedback", "Feedback")
                        .WithMany("CourseFeedbacks")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Exam", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.User", "Center")
                        .WithMany("OwnExams")
                        .HasForeignKey("CenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.Course", "Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Center");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Payment", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Course", "Course")
                        .WithMany("Payments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Question", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.User", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.UserExam", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Exam", "Exam")
                        .WithMany("UserExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.User", "User")
                        .WithMany("UserExams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduLingual.Infrastructure.UserCourse", b =>
                {
                    b.HasOne("EduLingual.Domain.Entities.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduLingual.Domain.Entities.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Course", b =>
                {
                    b.Navigation("CourseFeedbacks");

                    b.Navigation("Exams");

                    b.Navigation("Payments");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseArea", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseCategory", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.CourseLanguage", b =>
                {
                    b.Navigation("CourseCategories");

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Exam", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserExams");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Feedback", b =>
                {
                    b.Navigation("CourseFeedbacks");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EduLingual.Domain.Entities.User", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("OwnCourses");

                    b.Navigation("OwnExams");

                    b.Navigation("Payments");

                    b.Navigation("UserCourses");

                    b.Navigation("UserExams");
                });
#pragma warning restore 612, 618
        }
    }
}
