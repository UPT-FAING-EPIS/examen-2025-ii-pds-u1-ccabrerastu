using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exam.Api.Data;
using Exam.Api.Models;


namespace Exam.Api.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
private readonly AppDbContext _db;
public ExamsController(AppDbContext db) { _db = db; }


[HttpPost]
public async Task<IActionResult> Create(Exam exam)
{
_db.Exams.Add(exam);
await _db.SaveChangesAsync();
return CreatedAtAction(nameof(GetById), new { id = exam.Id }, exam);
}


[HttpGet]
public async Task<IActionResult> List() => Ok(await _db.Exams.ToListAsync());


[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
var exam = await _db.Exams.FindAsync(id);
if (exam == null) return NotFound();
return Ok(exam);
}
}
}