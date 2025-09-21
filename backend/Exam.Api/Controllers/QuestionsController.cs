using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exam.Api.Data;
using Exam.Api.Models;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public QuestionsController(AppDbContext db)
        {
            _db = db;
        }

        // POST /questions — Crear pregunta
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            _db.Questions.Add(question);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByExamId),
                new { examId = question.ExamId }, question);
        }

        // GET /questions/{examId} — Listar preguntas de un examen
        [HttpGet("{examId}")]
        public async Task<IActionResult> GetByExamId(int examId)
        {
            var questions = await _db.Questions
                                     .Where(q => q.ExamId == examId)
                                     .ToListAsync();
            return Ok(questions);
        }
    }
}
