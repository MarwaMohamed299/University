using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;

namespace UniversitySystem.Context;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<StudentTma> StudentTmas { get; set; }

    public virtual DbSet<Tma> Tmas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database= UniversityDB;Integrated Security=True; Encrypt = false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK_Contact_1");

            entity.ToTable("Contact");

            entity.Property(e => e.StudentId).HasMaxLength(50);
            entity.Property(e => e.CourseId).HasMaxLength(50);
            entity.Property(e => e.DateForNextContact).HasColumnType("date");
            entity.Property(e => e.Duration).ValueGeneratedOnAdd();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CousrseId);

            entity.ToTable("Course");

            entity.Property(e => e.CousrseId).HasMaxLength(50);
            entity.Property(e => e.CourseName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasMaxLength(50);
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.EmailAddress).HasMaxLength(50);
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("LName");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.StudentId }).HasName("PK_StudentCourse_1");

            entity.ToTable("StudentCourse");

            entity.Property(e => e.CourseId).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_StudentCourse_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourse_Student");
        });

        modelBuilder.Entity<StudentTma>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId, e.Tmaid });

            entity.ToTable("StudentTMA");

            entity.Property(e => e.StudentId).HasMaxLength(50);
            entity.Property(e => e.CourseId).HasMaxLength(50);
            entity.Property(e => e.Tmaid)
                .HasMaxLength(50)
                .HasColumnName("TMAId");
            entity.Property(e => e.DateIn).HasColumnType("date");
            entity.Property(e => e.DateOut).HasColumnType("date");
            entity.Property(e => e.Grade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentTmagradeComment).HasColumnName("StudentTMAGradeComment");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentTmas)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentTMA_Student");
        });

        modelBuilder.Entity<Tma>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.Tmaid });

            entity.ToTable("TMA");

            entity.Property(e => e.CourseId).HasMaxLength(50);
            entity.Property(e => e.Tmaid)
                .HasMaxLength(50)
                .HasColumnName("TMAId");
            entity.Property(e => e.Tmaletter)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TMALetter");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
