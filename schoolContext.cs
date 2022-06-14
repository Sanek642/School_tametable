using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace School_tametable
{
    public partial class schoolContext : DbContext
    {
        public schoolContext()
        {
        }

        public schoolContext(DbContextOptions<schoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeSubject> EmployeeSubjects { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<LessonsTime> LessonsTimes { get; set; } = null!;
        public virtual DbSet<NameClass> NameClasses { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlite(" Data Source= C:\\Users\\Alexa\\OneDrive\\Program\\School_tametable\\DB\\school.db ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassesId);

                entity.HasIndex(e => new { e.NameClassesId, e.EmployeeSubjectId, e.Cabinet, e.HoursPerWeek }, "IX_Classes_Name_classes_id_Employee_Subject_id_cabinet_hours_per_week")
                    .IsUnique();

                entity.HasIndex(e => e.ClassesId, "index_Classes_id1");

                entity.HasIndex(e => e.EmployeeSubjectId, "index_Employee_Subject_id");

                entity.HasIndex(e => e.NameClassesId, "index_Name_classes_id");

                entity.Property(e => e.ClassesId)
                    .HasColumnType("Integer")
                    .HasColumnName("Classes_id");

                entity.Property(e => e.Cabinet)
                    .HasColumnType("Text")
                    .HasColumnName("cabinet");

                entity.Property(e => e.EmployeeSubjectId)
                    .HasColumnType("Integer")
                    .HasColumnName("Employee_Subject_id");

                entity.Property(e => e.HoursPerWeek)
                    .HasColumnType("Integer")
                    .HasColumnName("hours_per_week");

                entity.Property(e => e.NameClassesId)
                    .HasColumnType("Integer")
                    .HasColumnName("Name_classes_id");

                entity.HasOne(d => d.EmployeeSubject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.EmployeeSubjectId);

                entity.HasOne(d => d.NameClasses)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.NameClassesId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployess);

                entity.HasIndex(e => e.Email, "IX_Employees_email")
                    .IsUnique();

                entity.HasIndex(e => e.NameEmployess, "IX_Employees_name_employess")
                    .IsUnique();

                entity.Property(e => e.IdEmployess)
                    .HasColumnType("Integer")
                    .HasColumnName("id_employess");

                entity.Property(e => e.Email)
                    .HasColumnType("Text")
                    .HasColumnName("email");

                entity.Property(e => e.NameEmployess)
                    .HasColumnType("Text")
                    .HasColumnName("name_employess");
            });

            modelBuilder.Entity<EmployeeSubject>(entity =>
            {
                entity.HasKey(e => e.IdEs);

                entity.ToTable("Employee_Subject");

                entity.HasIndex(e => new { e.EmployeesId, e.SubjectsId }, "IX_Employee_Subject_Employees_id_Subjects_id")
                    .IsUnique();

                entity.Property(e => e.IdEs)
                    .HasColumnType("Integer")
                    .HasColumnName("id_es");

                entity.Property(e => e.EmployeesId)
                    .HasColumnType("Integer")
                    .HasColumnName("Employees_id");

                entity.Property(e => e.SubjectsId)
                    .HasColumnType("Integer")
                    .HasColumnName("Subjects_id");

                entity.HasOne(d => d.Employees)
                    .WithMany(p => p.EmployeeSubjects)
                    .HasForeignKey(d => d.EmployeesId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Subjects)
                    .WithMany(p => p.EmployeeSubjects)
                    .HasForeignKey(d => d.SubjectsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.IdLesson);

                entity.HasIndex(e => e.ClassesId, "index_Classes_id");

                entity.HasIndex(e => e.LessonsTimeId, "index_lessons_time_id");

                entity.Property(e => e.IdLesson)
                    .HasColumnType("Integer")
                    .HasColumnName("id_lesson");

                entity.Property(e => e.ClassesId)
                    .HasColumnType("Integer")
                    .HasColumnName("Classes_id");

                entity.Property(e => e.LessonsTimeId)
                    .HasColumnType("Integer")
                    .HasColumnName("lessons_time_id");

                entity.HasOne(d => d.Classes)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ClassesId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.LessonsTime)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonsTimeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LessonsTime>(entity =>
            {
                entity.HasKey(e => e.IdLt);

                entity.ToTable("lessons_time");

                entity.HasIndex(e => new { e.DayOfWeek, e.Change, e.Number, e.TimeEnd, e.TimeBeg }, "IX_lessons_time_day_of_week_change_number_time_end_time_beg")
                    .IsUnique();

                entity.Property(e => e.IdLt)
                    .HasColumnType("Integer")
                    .HasColumnName("id_lt");

                entity.Property(e => e.Change)
                    .HasColumnType("Integer")
                    .HasColumnName("change");

                entity.Property(e => e.DayOfWeek)
                    .HasColumnType("Text")
                    .HasColumnName("day_of_week");

                entity.Property(e => e.Number)
                    .HasColumnType("Integer")
                    .HasColumnName("number");

                entity.Property(e => e.TimeBeg)
                    .HasColumnType("DateTime")
                    .HasColumnName("time_beg");

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("DateTime")
                    .HasColumnName("time_end");
            });

            modelBuilder.Entity<NameClass>(entity =>
            {
                entity.HasKey(e => e.IdNameCl);

                entity.ToTable("Name_classes");

                entity.HasIndex(e => e.NameClass1, "IX_Name_classes_name_class")
                    .IsUnique();

                entity.Property(e => e.IdNameCl)
                    .HasColumnType("Integer")
                    .HasColumnName("id_name_cl");

                entity.Property(e => e.NameClass1)
                    .HasColumnType("Text")
                    .HasColumnName("name_class");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubjects);

                entity.HasIndex(e => e.NameSubject, "IX_Subjects_name_subject")
                    .IsUnique();

                entity.Property(e => e.IdSubjects)
                    .HasColumnType("Integer")
                    .HasColumnName("id_subjects");

                entity.Property(e => e.NameSubject)
                    .HasColumnType("Text")
                    .HasColumnName("name_subject");

                entity.Property(e => e.Share)
                    .HasColumnType("Integer")
                    .HasColumnName("share");

                entity.Property(e => e.Successively)
                    .HasColumnType("Integer")
                    .HasColumnName("successively");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
