using Microsoft.AspNetCore.Mvc;
using Exam.Api.Data;
using Exam.Api.Models;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SubmissionsController(AppDbContext db)
        {
            _db = db;
        }

        // POST /submissions — Enviar respuestas
        [HttpPost]
        public async Task<IActionResult> Submit(Submission submission)
        {
            _db.Submissions.Add(submission);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Respuestas enviadas correctamente." });
        }
    }
}
