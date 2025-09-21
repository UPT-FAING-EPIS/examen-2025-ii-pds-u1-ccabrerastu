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

        public ExamsController(AppDbContext db)
        {
            _db = db;
        }

        // Crear un examen
        [HttpPost]
        public async Task<IActionResult> Create(ExamModel exam)
        {
            _db.Exams.Add(exam);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = exam.Id }, exam);
        }

        // Listar todos los exámenes
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var exams = await _db.Exams.ToListAsync();
            return Ok(exams);
        }

        // Obtener examen por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _db.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }
    }
}
