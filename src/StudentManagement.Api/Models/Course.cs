namespace StudentManagement.Api.Models;

public class Course
{
    public int Id { get; set; }                 // PK
    public string Title { get; set; } = default!;
    public int Credits { get; set; }            // fx 5 eller 10

    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}