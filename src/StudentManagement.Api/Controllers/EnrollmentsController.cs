using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController(SchoolContext db) : ControllerBase
{
    // DTO til POST for at holde body enkel
    public record EnrollmentRequest(int StudentId, int CourseId, int? Grade);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> Get()
    {
        // Returnér med lidt "join" info (Student + Course)
        var items = await db.Enrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Select(e => new {
                e.Id,
                e.StudentId,
                Student = e.Student != null ? (e.Student.FirstName + " " + e.Student.LastName) : null,
                e.CourseId,
                Course = e.Course != null ? e.Course.Title : null,
                e.EnrolledAt,
                e.Grade
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<object>> GetById(int id)
    {
        var e = await db.Enrollments
            .AsNoTracking()
            .Include(x => x.Student)
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (e is null) return NotFound();

        return Ok(new {
            e.Id,
            e.StudentId,
            Student = e.Student != null ? (e.Student.FirstName + " " + e.Student.LastName) : null,
            e.CourseId,
            Course = e.Course != null ? e.Course.Title : null,
            e.EnrolledAt,
            e.Grade
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post(EnrollmentRequest req)
    {
        // Tjek at Student og Course findes (god praksis)
        var studentExists = await db.Students.AnyAsync(s => s.Id == req.StudentId);
        var courseExists  = await db.Courses.AnyAsync(c => c.Id == req.CourseId);
        if (!studentExists || !courseExists)
            return BadRequest("Invalid StudentId or CourseId.");

        var enrollment = new Enrollment
        {
            StudentId = req.StudentId,
            CourseId = req.CourseId,
            Grade = req.Grade,
            EnrolledAt = DateTime.UtcNow
        };

        db.Enrollments.Add(enrollment);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            // Rammer vores unikke index (StudentId, CourseId)
            return Conflict("This student is already enrolled in this course.");
        }

        return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, new {
            enrollment.Id,
            enrollment.StudentId,
            enrollment.CourseId,
            enrollment.EnrolledAt,
            enrollment.Grade
        });
    }
}
