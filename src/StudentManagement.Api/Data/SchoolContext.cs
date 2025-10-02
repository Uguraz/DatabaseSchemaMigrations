using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Data;

public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Instructor> Instructors => Set<Instructor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");
            entity.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.EnrollmentDate).IsRequired();
        });

        // Department
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments");
            entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Budget).HasColumnType("decimal(18,2)").IsRequired();

            // Seed fallback (Id = 1)
            entity.HasData(new Department { Id = 1, Name = "General", Budget = 0m });
        });

        // Instructor
        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.ToTable("Instructors");
            entity.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.HireDate).IsRequired();

            // Seed fallback (Id = 1)
            entity.HasData(new Instructor { Id = 1, FirstName = "TBD", LastName = "Instructor", HireDate = new DateTime(2000, 1, 1) });
        });

        // Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");
            entity.Property(p => p.Title).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Credits).IsRequired();

            // Defaults så eksisterende rækker får gyldige FK'er
            entity.Property(p => p.DepartmentId).HasDefaultValue(1);
            entity.Property(p => p.InstructorId).HasDefaultValue(1);

            entity.HasOne(c => c.Department)
                  .WithMany(d => d.Courses)
                  .HasForeignKey(c => c.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Instructor)
                  .WithMany(i => i.Courses)
                  .HasForeignKey(c => c.InstructorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Enrollment
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollments");

            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Enrollments)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
        });
    }
}