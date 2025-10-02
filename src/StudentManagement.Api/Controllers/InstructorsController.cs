using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController(SchoolContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instructor>>> Get()
        => await db.Instructors.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Instructor>> GetById(int id)
    {
        var i = await db.Instructors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return i is null ? NotFound() : Ok(i);
    }

    [HttpPost]
    public async Task<ActionResult<Instructor>> Post(Instructor i)
    {
        db.Instructors.Add(i);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = i.Id }, i);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var i = await db.Instructors.FindAsync(id);
        if (i is null) return NotFound();
        db.Instructors.Remove(i);
        await db.SaveChangesAsync();
        return NoContent();
    }
}