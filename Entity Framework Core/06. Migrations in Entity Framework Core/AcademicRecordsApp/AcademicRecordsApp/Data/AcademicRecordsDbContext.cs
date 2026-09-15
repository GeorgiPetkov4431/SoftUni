using System;
using System.Collections.Generic;
using AcademicRecordsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicRecordsApp.Data
{
    public class AcademicRecordsDbContext : DbContext
    {
        // Ids need to be hardcoded, because this seeding does not go through the change tracker, and is done through raw SQL
        private Course[] seedCourses = new Course[]
        {
            new Course { Id = 1, Name = "Mathematics" },
            new Course { Id = 2, Name = "Physics" },
            new Course { Id = 3, Name = "Chemistry" },
            new Course { Id = 4, Name = "Biology" },
            new Course { Id = 5, Name = "Computer Science" },
            new Course { Id = 6, Name = "History" },
            new Course { Id = 7, Name = "Literature" },
            new Course { Id = 8, Name = "Geography" },
            new Course { Id = 9, Name = "Philosophy" },
            new Course { Id = 10, Name = "Economics" }
        };

        public AcademicRecordsDbContext()
        {
        }

        public AcademicRecordsDbContext(DbContextOptions<AcademicRecordsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exams { get; set; } = null!;

        public virtual DbSet<Grade> Grades { get; set; } = null!;

        public virtual DbSet<Student> Students { get; set; } = null!;

        public virtual DbSet<Course> Courses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Exams__3214EC07DD612108");

                //entity.Property(e => e.Name)
                //    .HasMaxLength(100);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity
                    .HasKey(e => e.Id)
                    .HasName("PK__Grades__3214EC07B60F8E6B");

                //entity
                //    .Property(e => e.Value)
                //    .HasColumnType("decimal(3, 2)");

                entity
                    .HasOne(d => d.Exam)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades_Exams");

                entity
                    .HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades_Students");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity
                    .HasKey(e => e.Id)
                    .HasName("PK__Students__3214EC07F540558B");

                //entity
                //    .Property(e => e.FullName)
                //    .HasMaxLength(100);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity
                    .HasKey(e => e.Id);

                //entity
                //    .Property(e => e.Name)
                //    .IsRequired()
                //    .IsUnicode()
                //    .HasMaxLength(100);

                entity
                    .HasData(seedCourses);

                entity.HasMany(e => e.Students)
                    .WithMany(s => s.Courses)
                    .UsingEntity("StudentsCourses",
                        r => r.HasOne(typeof(Student)).WithMany().HasForeignKey("StudentId").HasPrincipalKey(nameof(Student.Id)),
                        l => l.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseId").HasPrincipalKey(nameof(Course.Id)),
                        j => j.HasKey("StudentId", "CourseId"));
            });
        }
    }

}