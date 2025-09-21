using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureSubmissionExamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersJson",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "Submissions",
                newName: "SubmittedAt");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Submissions",
                newName: "StartedAt");

            migrationBuilder.AddColumn<string>(
                name: "AnswersJson",
                table: "Submissions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "Submissions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalScore",
                table: "Submissions",
                type: "decimal(65,30)",
                nullable: true);
        }
    }
}
