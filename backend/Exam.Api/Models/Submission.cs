using System.ComponentModel.DataAnnotations;


namespace Exam.Api.Models
{
public class Submission
{
[Key]
public int Id { get; set; }
public int ExamId { get; set; }
public int UserId { get; set; }
public DateTime StartedAt { get; set; }
public DateTime? FinishedAt { get; set; }
public string AnswersJson { get; set; }
public decimal? TotalScore { get; set; }
}
}