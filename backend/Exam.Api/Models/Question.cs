using System.ComponentModel.DataAnnotations;


namespace Exam.Api.Models
{
public class Question
{
[Key]
public int Id { get; set; }
public int ExamId { get; set; }
public string Type { get; set; } // multiple_choice | true_false | open
public string Text { get; set; }
public string? OptionsJson { get; set; }
public string? CorrectJson { get; set; }
public decimal Score { get; set; } = 1;
}
}