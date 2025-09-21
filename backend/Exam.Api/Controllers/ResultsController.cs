using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exam.Api.Data;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ResultsController(AppDbContext db)
        {
            _db = db;
        }

        // GET /results/{userId} — Ver resultados de un usuario
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResults(int userId)
        {
            var results = await _db.Submissions
                                   .Include(s => s.Exam)
                                   .Where(s => s.UserId == userId)
                                   .ToListAsync();

            return Ok(results);
        }
    }
}
