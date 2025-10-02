namespace StudentManagement.Api.Models;

public class Course
{
    public int Id { get; set; }                 // PK
    public string Title { get; set; } = default!;
    public int Credits { get; set; }            

    public int DepartmentId { get; set; }       // FK
    public int InstructorId { get; set; }       // FK

    // Navigation
    public Department? Department { get; set; }
    public Instructor? Instructor { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}