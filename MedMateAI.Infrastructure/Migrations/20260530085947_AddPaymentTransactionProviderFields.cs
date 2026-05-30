using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTransactionProviderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "PaymentTransaction",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "PaymentTransaction",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderInfo",
                table: "PaymentTransaction",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "PaymentTransaction",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedAt",
                table: "PaymentTransaction",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderResponseCode",
                table: "PaymentTransaction",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderTransactionId",
                table: "PaymentTransaction",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderTransactionStatus",
                table: "PaymentTransaction",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "PaymentTransaction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionReference",
                table: "PaymentTransaction",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_PaymentId",
                table: "PaymentTransaction",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_ProviderTransactionId",
                table: "PaymentTransaction",
                column: "ProviderTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_TransactionReference",
                table: "PaymentTransaction",
                column: "TransactionReference");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransaction_Payment_PaymentId",
                table: "PaymentTransaction",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransaction_Payment_PaymentId",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_PaymentId",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_ProviderTransactionId",
                table: "PaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_TransactionReference",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "OrderInfo",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ProviderResponseCode",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ProviderTransactionId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ProviderTransactionStatus",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionReference",
                table: "PaymentTransaction");
        }
    }
}
