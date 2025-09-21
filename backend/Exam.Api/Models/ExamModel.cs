// 📄 ExamModel.cs
using System.ComponentModel.DataAnnotations;

namespace Exam.Api.Models
{
    public class ExamModel
    {
        [Key]  // Marca la clave primaria
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public int CreatedBy { get; set; }
    }
}
