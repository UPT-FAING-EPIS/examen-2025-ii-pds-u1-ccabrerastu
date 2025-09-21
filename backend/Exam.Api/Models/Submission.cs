using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Api.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        // Clave foránea al examen
        public int ExamId { get; set; }

        // ✅ Propiedad de navegación para el Include
        [ForeignKey("ExamId")]
        public ExamModel Exam { get; set; } = null!;

        public DateTime SubmittedAt { get; set; }
        public int Score { get; set; }
    }
}
