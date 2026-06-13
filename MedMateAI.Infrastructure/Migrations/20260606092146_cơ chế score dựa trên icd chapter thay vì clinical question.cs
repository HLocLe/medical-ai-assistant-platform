using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cơchếscoredựatrênicdchapterthayvìclinicalquestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeywordWeights",
                table: "ClinicalQuestions");

            migrationBuilder.AddColumn<string>(
                name: "KeywordWeights",
                table: "IcdChapters",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeywordWeights",
                table: "IcdChapters");

            migrationBuilder.AddColumn<Dictionary<string, int>>(
                name: "KeywordWeights",
                table: "ClinicalQuestions",
                type: "jsonb",
                nullable: false);
        }
    }
}
