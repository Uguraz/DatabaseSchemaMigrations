namespace StudentManagement.Api.Models;

public class Enrollment
{
    public int Id { get; set; }                 // PK (surrogate key)
    public int StudentId { get; set; }          // FK -> Student
    public int CourseId { get; set; }           // FK -> Course
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public int? Grade { get; set; }             // valgfrit felt (0-100)

    // Navigation
    public Student? Student { get; set; }
    public Course? Course { get; set; }
}