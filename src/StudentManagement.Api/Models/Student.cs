namespace StudentManagement.Api.Models;

public class Student
{
    public int Id { get; set; }                 // Primær nøgle (PK)
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime EnrollmentDate { get; set; }
}