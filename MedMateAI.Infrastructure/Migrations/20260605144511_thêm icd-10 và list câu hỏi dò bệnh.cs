using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thêmicd10vàlistcâuhỏidòbệnh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterKeywords");

            migrationBuilder.AddColumn<string>(
                name: "ChapterCode",
                table: "MedicalDepartment",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Dictionary<string, int>>(
                name: "KeywordWeights",
                table: "ClinicalQuestions",
                type: "jsonb",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDepartment_ChapterCode",
                table: "MedicalDepartment",
                column: "ChapterCode");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDepartment_IcdChapters_ChapterCode",
                table: "MedicalDepartment",
                column: "ChapterCode",
                principalTable: "IcdChapters",
                principalColumn: "ChapterCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDepartment_IcdChapters_ChapterCode",
                table: "MedicalDepartment");

            migrationBuilder.DropIndex(
                name: "IX_MedicalDepartment_ChapterCode",
                table: "MedicalDepartment");

            migrationBuilder.DropColumn(
                name: "ChapterCode",
                table: "MedicalDepartment");

            migrationBuilder.DropColumn(
                name: "KeywordWeights",
                table: "ClinicalQuestions");

            migrationBuilder.CreateTable(
                name: "ChapterKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IcdChapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Keyword = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterKeywords_IcdChapters_IcdChapterId",
                        column: x => x.IcdChapterId,
                        principalTable: "IcdChapters",
                        principalColumn: "IcdChapterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterKeywords_IcdChapterId_Keyword",
                table: "ChapterKeywords",
                columns: new[] { "IcdChapterId", "Keyword" },
                unique: true);
        }
    }
}
