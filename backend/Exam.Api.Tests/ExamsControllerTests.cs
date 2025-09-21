using System.Collections.Generic;
using System.Threading.Tasks;
using Exam.Api.Controllers;
using Exam.Api.Data;
using Exam.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exam.Api.Tests
{
    public class ExamsControllerTests
    {
        private AppDbContext GetInMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task List_ReturnsAllExams()
        {
            // Arrangesss
            using var db = GetInMemoryDb(nameof(List_ReturnsAllExams));
            db.Exams.AddRange(
                new ExamModel { Id = 1, Title = "Matemáticas", Description = "Álgebra", DurationMinutes = 30 },
                new ExamModel { Id = 2, Title = "Historia", Description = "Edad Media", DurationMinutes = 25 }
            );
            await db.SaveChangesAsync();
            var controller = new ExamsController(db);

            // Act
            var result = await controller.List();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var exams = Assert.IsAssignableFrom<List<ExamModel>>(okResult.Value);
            exams.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ReturnsExam_WhenExists()
        {
            using var db = GetInMemoryDb(nameof(GetById_ReturnsExam_WhenExists));
            var exam = new ExamModel { Id = 3, Title = "Química", Description = "Ácidos", DurationMinutes = 40 };
            db.Exams.Add(exam);
            await db.SaveChangesAsync();
            var controller = new ExamsController(db);

            var result = await controller.GetById(3);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedExam = Assert.IsType<ExamModel>(okResult.Value);
            returnedExam.Title.Should().Be("Química");
            returnedExam.Description.Should().Be("Ácidos");
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenDoesNotExist()
        {
            using var db = GetInMemoryDb(nameof(GetById_ReturnsNotFound_WhenDoesNotExist));
            var controller = new ExamsController(db);

            var result = await controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_AddsExamAndReturnsCreated()
        {
            using var db = GetInMemoryDb(nameof(Create_AddsExamAndReturnsCreated));
            var controller = new ExamsController(db);
            var newExam = new ExamModel { Title = "Física", Description = "Mecánica", DurationMinutes = 35 };

            var result = await controller.Create(newExam);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var createdExam = Assert.IsType<ExamModel>(created.Value);

            createdExam.Title.Should().Be("Física");
            createdExam.Id.Should().BeGreaterThan(0);
            (await db.Exams.CountAsync()).Should().Be(1);
        }
    }
}
