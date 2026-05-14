using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fieldsadjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyContactName",
                table: "PatientProfile");

            migrationBuilder.DropColumn(
                name: "EmergencyContactPhone",
                table: "PatientProfile");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserSubscription",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserMedication",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TreatmentLog",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TreatmentJourney",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SymptomAnalysisSession",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SubscriptionPlan",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SessionSymptom",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RecoveryPlan",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PaymentTransaction",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PatientProfile",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Notification",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Medicine",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicationScanResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicationScan",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicalVisit",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicalRecordFile",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicalRecord",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicalFacility",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MedicalDepartment",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "LabResultDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "LabResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "KnowledgeSource",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "KnowledgeDocument",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "KnowledgeChunk",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FollowUpReminder",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FeedbackReview",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FacilityDepartment",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DrugAnalysisResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DrugAnalysis",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Doctor",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DepartmentRecommendation",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ConsultationSession",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ConsultationQuestion",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstLoginAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AISystemConfig",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AIAnalysis",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserSubscription");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserMedication");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TreatmentLog");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TreatmentJourney");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SymptomAnalysisSession");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SubscriptionPlan");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SessionSymptom");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RecoveryPlan");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PatientProfile");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicationScanResult");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicationScan");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicalVisit");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicalRecordFile");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicalFacility");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MedicalDepartment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "LabResultDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "LabResult");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "KnowledgeSource");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "KnowledgeDocument");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "KnowledgeChunk");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FollowUpReminder");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FeedbackReview");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FacilityDepartment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DrugAnalysisResult");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DrugAnalysis");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DepartmentRecommendation");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ConsultationSession");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ConsultationQuestion");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstLoginAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AISystemConfig");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AIAnalysis");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactName",
                table: "PatientProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactPhone",
                table: "PatientProfile",
                type: "text",
                nullable: true);
        }
    }
}
