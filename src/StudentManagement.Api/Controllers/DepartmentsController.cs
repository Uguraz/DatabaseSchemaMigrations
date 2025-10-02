using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(SchoolContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> Get()
        => await db.Departments.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Department>> GetById(int id)
    {
        var d = await db.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return d is null ? NotFound() : Ok(d);
    }

    [HttpPost]
    public async Task<ActionResult<Department>> Post(Department d)
    {
        db.Departments.Add(d);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = d.Id }, d);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var d = await db.Departments.FindAsync(id);
        if (d is null) return NotFound();
        db.Departments.Remove(d);
        await db.SaveChangesAsync();
        return NoContent();
    }
}