using Microsoft.EntityFrameworkCore;
using Exam.Api.Models;


namespace Exam.Api.Data
{
public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<User> Users { get; set; }
public DbSet<Exam> Exams { get; set; }
public DbSet<Question> Questions { get; set; }
public DbSet<Submission> Submissions { get; set; }
}
}