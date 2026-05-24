using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceModelParamsWithAiFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelParams",
                table: "AISystemConfig");

            migrationBuilder.AddColumn<int>(
                name: "MaxTokens",
                table: "AISystemConfig",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "AISystemConfig",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temperature",
                table: "AISystemConfig",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTokens",
                table: "AISystemConfig");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "AISystemConfig");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "AISystemConfig");

            migrationBuilder.AddColumn<string>(
                name: "ModelParams",
                table: "AISystemConfig",
                type: "text",
                nullable: true);
        }
    }
}
