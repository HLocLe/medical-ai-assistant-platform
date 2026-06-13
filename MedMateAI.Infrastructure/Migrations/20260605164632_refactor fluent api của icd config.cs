using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorfluentapicủaicdconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalQuestions_IcdChapters_ChapterCode",
                table: "ClinicalQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalQuestions_ChapterCode_SortOrder",
                table: "ClinicalQuestions");

            migrationBuilder.DropColumn(
                name: "ChapterCode",
                table: "ClinicalQuestions");

            migrationBuilder.AddColumn<Guid>(
                name: "ChapterId",
                table: "ClinicalQuestions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalQuestions_ChapterId_SortOrder",
                table: "ClinicalQuestions",
                columns: new[] { "ChapterId", "SortOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalQuestions_IcdChapters_ChapterId",
                table: "ClinicalQuestions",
                column: "ChapterId",
                principalTable: "IcdChapters",
                principalColumn: "IcdChapterId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalQuestions_IcdChapters_ChapterId",
                table: "ClinicalQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalQuestions_ChapterId_SortOrder",
                table: "ClinicalQuestions");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                table: "ClinicalQuestions");

            migrationBuilder.AddColumn<string>(
                name: "ChapterCode",
                table: "ClinicalQuestions",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalQuestions_ChapterCode_SortOrder",
                table: "ClinicalQuestions",
                columns: new[] { "ChapterCode", "SortOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalQuestions_IcdChapters_ChapterCode",
                table: "ClinicalQuestions",
                column: "ChapterCode",
                principalTable: "IcdChapters",
                principalColumn: "ChapterCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
