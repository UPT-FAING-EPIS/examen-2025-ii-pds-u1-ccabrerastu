using Microsoft.EntityFrameworkCore;
using Exam.Api.Models;

namespace Exam.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ExamModel> Exams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        // ✅ Agrega esta configuración
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Submission → ExamModel
            modelBuilder.Entity<Submission>()
                        .HasOne(s => s.Exam)          // Una Submission tiene un Exam
                        .WithMany()                   // Un Exam puede tener muchas Submissions (si tienes la colección, cámbialo a .WithMany(e => e.Submissions))
                        .HasForeignKey(s => s.ExamId) // Clave foránea
                        .OnDelete(DeleteBehavior.Cascade); // Elimina submissions si se elimina un examen

            // Si tienes más relaciones, configúralas aquí.
        }
    }
}
