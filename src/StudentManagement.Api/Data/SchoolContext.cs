using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Data;

public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

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

        // Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");
            entity.Property(p => p.Title).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Credits).IsRequired();
        });

        // Enrollment (join)
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollments");

            entity.HasOne(e => e.Student)
                .WithMany()                         // vi har ikke collection på Student endnu
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Undgå duplikate tilmeldinger til samme kursus
            entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
        });
    }
}