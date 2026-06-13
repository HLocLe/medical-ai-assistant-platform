using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionClinicalQuestionAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionClinicalQuestionAnswer",
                columns: table => new
                {
                    SessionClinicalQuestionAnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicalQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionClinicalQuestionAnswer", x => x.SessionClinicalQuestionAnswerId);
                    table.ForeignKey(
                        name: "FK_SessionClinicalQuestionAnswer_ClinicalQuestions_ClinicalQue~",
                        column: x => x.ClinicalQuestionId,
                        principalTable: "ClinicalQuestions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionClinicalQuestionAnswer_SymptomAnalysisSession_Sympto~",
                        column: x => x.SymptomAnalysisSessionId,
                        principalTable: "SymptomAnalysisSession",
                        principalColumn: "SymptomAnalysisSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionClinicalQuestionAnswer_ClinicalQuestionId",
                table: "SessionClinicalQuestionAnswer",
                column: "ClinicalQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionClinicalQuestionAnswer_SymptomAnalysisSessionId_Clin~",
                table: "SessionClinicalQuestionAnswer",
                columns: new[] { "SymptomAnalysisSessionId", "ClinicalQuestionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionClinicalQuestionAnswer");
        }
    }
}
