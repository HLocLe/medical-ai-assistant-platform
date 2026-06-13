using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIcdChapterClinicalQuestionChapterKeyword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeChunk");

            migrationBuilder.DropTable(
                name: "KnowledgeDocument");

            migrationBuilder.DropTable(
                name: "KnowledgeSource");

            migrationBuilder.AddColumn<string>(
                name: "ChapterCode",
                table: "SymptomAnalysisSession",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

           

            migrationBuilder.CreateTable(
                name: "IcdChapters",
                columns: table => new
                {
                    IcdChapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChapterCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ChapterName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcdChapters", x => x.IcdChapterId);
                    table.UniqueConstraint("AK_IcdChapters_ChapterCode", x => x.ChapterCode);
                });

            migrationBuilder.CreateTable(
                name: "ChapterKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Keyword = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IcdChapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ClinicalQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChapterCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    QuestionVi = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    EnglishPrefix = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_ClinicalQuestions_IcdChapters_ChapterCode",
                        column: x => x.ChapterCode,
                        principalTable: "IcdChapters",
                        principalColumn: "ChapterCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymptomAnalysisSession_ChapterCode",
                table: "SymptomAnalysisSession",
                column: "ChapterCode");

           

            migrationBuilder.CreateIndex(
                name: "IX_ChapterKeywords_IcdChapterId_Keyword",
                table: "ChapterKeywords",
                columns: new[] { "IcdChapterId", "Keyword" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalQuestions_ChapterCode_SortOrder",
                table: "ClinicalQuestions",
                columns: new[] { "ChapterCode", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_IcdChapters_ChapterCode",
                table: "IcdChapters",
                column: "ChapterCode",
                unique: true);

           

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomAnalysisSession_IcdChapters_ChapterCode",
                table: "SymptomAnalysisSession",
                column: "ChapterCode",
                principalTable: "IcdChapters",
                principalColumn: "ChapterCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomAnalysisSession_IcdChapters_ChapterCode",
                table: "SymptomAnalysisSession");

            migrationBuilder.DropTable(
                name: "ChapterKeywords");

            migrationBuilder.DropTable(
                name: "ClinicalQuestions");

            migrationBuilder.DropTable(
                name: "IcdChapters");

            migrationBuilder.DropIndex(
                name: "IX_SymptomAnalysisSession_ChapterCode",
                table: "SymptomAnalysisSession");

          

            migrationBuilder.DropColumn(
                name: "ChapterCode",
                table: "SymptomAnalysisSession");

            

            migrationBuilder.CreateTable(
                name: "KnowledgeSource",
                columns: table => new
                {
                    KnowledgeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SourceName = table.Column<string>(type: "text", nullable: true),
                    SourceType = table.Column<string>(type: "text", nullable: true),
                    TrustLevel = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeSource", x => x.KnowledgeSourceId);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeDocument",
                columns: table => new
                {
                    KnowledgeDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnowledgeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeDocument", x => x.KnowledgeDocumentId);
                    table.ForeignKey(
                        name: "FK_KnowledgeDocument_KnowledgeSource_KnowledgeSourceId",
                        column: x => x.KnowledgeSourceId,
                        principalTable: "KnowledgeSource",
                        principalColumn: "KnowledgeSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeChunk",
                columns: table => new
                {
                    KnowledgeChunkId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnowledgeDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChunkText = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmbeddingVectorReference = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PageNumber = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeChunk", x => x.KnowledgeChunkId);
                    table.ForeignKey(
                        name: "FK_KnowledgeChunk_KnowledgeDocument_KnowledgeDocumentId",
                        column: x => x.KnowledgeDocumentId,
                        principalTable: "KnowledgeDocument",
                        principalColumn: "KnowledgeDocumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeChunk_KnowledgeDocumentId",
                table: "KnowledgeChunk",
                column: "KnowledgeDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeDocument_KnowledgeSourceId",
                table: "KnowledgeDocument",
                column: "KnowledgeSourceId");
        }
    }
}
