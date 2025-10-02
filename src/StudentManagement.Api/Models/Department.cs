namespace StudentManagement.Api.Models;

public class Department
{
    public int Id { get; set; }                 // PK
    public string Name { get; set; } = default!;
    public decimal Budget { get; set; }         // fx 100000.00

    // Navigation
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}