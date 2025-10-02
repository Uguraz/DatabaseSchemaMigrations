namespace StudentManagement.Api.Models;

public class Instructor
{
    public int Id { get; set; }                 // PK
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}