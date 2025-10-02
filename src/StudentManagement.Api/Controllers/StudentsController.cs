using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(SchoolContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get()
        => await db.Students.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student s)
    {
        db.Students.Add(s);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = s.Id }, s);
    }
}