using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMateAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PatientProfileAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnowledgeSource",
                columns: table => new
                {
                    KnowledgeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceName = table.Column<string>(type: "text", nullable: true),
                    SourceType = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    TrustLevel = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeSource", x => x.KnowledgeSourceId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalDepartment",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDepartment", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalFacility",
                columns: table => new
                {
                    FacilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    OpeningHours = table.Column<string>(type: "text", nullable: true),
                    FacilityType = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalFacility", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "MedicationScan",
                columns: table => new
                {
                    MedicationScanId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ExtractedText = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationScan", x => x.MedicationScanId);
                    table.ForeignKey(
                        name: "FK_MedicationScan_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineName = table.Column<string>(type: "text", nullable: true),
                    ActiveIngredient = table.Column<string>(type: "text", nullable: true),
                    DosageForm = table.Column<string>(type: "text", nullable: true),
                    Strength = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineId);
                });

            migrationBuilder.CreateTable(
                name: "PatientProfile",
                columns: table => new
                {
                    PatientProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<double>(type: "double precision", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    AllergyNote = table.Column<string>(type: "text", nullable: true),
                    ChronicDiseaseNote = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfile", x => x.PatientProfileId);
                    table.ForeignKey(
                        name: "FK_PatientProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanName = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DurationInDays = table.Column<int>(type: "integer", nullable: false),
                    FeatureLimitJson = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "SymptomAnalysisSession",
                columns: table => new
                {
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InputText = table.Column<string>(type: "text", nullable: true),
                    SeverityLevel = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DisclaimerShown = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomAnalysisSession", x => x.SymptomAnalysisSessionId);
                    table.ForeignKey(
                        name: "FK_SymptomAnalysisSession_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeDocument",
                columns: table => new
                {
                    KnowledgeDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnowledgeSourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ContentUrl = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "FacilityDepartment",
                columns: table => new
                {
                    FacilityDepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityDepartment", x => x.FacilityDepartmentId);
                    table.ForeignKey(
                        name: "FK_FacilityDepartment_MedicalDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MedicalDepartment",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacilityDepartment_MedicalFacility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "MedicalFacility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationScanResult",
                columns: table => new
                {
                    MedicationScanResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicationScanId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfidenceScore = table.Column<double>(type: "double precision", nullable: true),
                    DetectedName = table.Column<string>(type: "text", nullable: true),
                    DetectedDosage = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationScanResult", x => x.MedicationScanResultId);
                    table.ForeignKey(
                        name: "FK_MedicationScanResult_MedicationScan_MedicationScanId",
                        column: x => x.MedicationScanId,
                        principalTable: "MedicationScan",
                        principalColumn: "MedicationScanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationScanResult_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscription",
                columns: table => new
                {
                    UserSubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    AutoRenew = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscription", x => x.UserSubscriptionId);
                    table.ForeignKey(
                        name: "FK_UserSubscription_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscription_SubscriptionPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRecommendation",
                columns: table => new
                {
                    RecommendationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfidenceScore = table.Column<double>(type: "double precision", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    PriorityRank = table.Column<int>(type: "integer", nullable: false),
                    IsEmergencySuggested = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentRecommendation", x => x.RecommendationId);
                    table.ForeignKey(
                        name: "FK_DepartmentRecommendation_MedicalDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MedicalDepartment",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentRecommendation_SymptomAnalysisSession_SymptomAnal~",
                        column: x => x.SymptomAnalysisSessionId,
                        principalTable: "SymptomAnalysisSession",
                        principalColumn: "SymptomAnalysisSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionSymptom",
                columns: table => new
                {
                    SessionSymptomId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomName = table.Column<string>(type: "text", nullable: true),
                    ConfidenceScore = table.Column<double>(type: "double precision", nullable: true),
                    ExtractedText = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSymptom", x => x.SessionSymptomId);
                    table.ForeignKey(
                        name: "FK_SessionSymptom_SymptomAnalysisSession_SymptomAnalysisSessio~",
                        column: x => x.SymptomAnalysisSessionId,
                        principalTable: "SymptomAnalysisSession",
                        principalColumn: "SymptomAnalysisSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeChunk",
                columns: table => new
                {
                    KnowledgeChunkId = table.Column<Guid>(type: "uuid", nullable: false),
                    KnowledgeDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChunkText = table.Column<string>(type: "text", nullable: true),
                    EmbeddingVectorReference = table.Column<string>(type: "text", nullable: true),
                    PageNumber = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityDepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Specialty = table.Column<string>(type: "text", nullable: true),
                    AcademicTitle = table.Column<string>(type: "text", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctor_FacilityDepartment_FacilityDepartmentId",
                        column: x => x.FacilityDepartmentId,
                        principalTable: "FacilityDepartment",
                        principalColumn: "FacilityDepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransaction",
                columns: table => new
                {
                    PaymentTransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserSubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentProvider = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransaction", x => x.PaymentTransactionId);
                    table.ForeignKey(
                        name: "FK_PaymentTransaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransaction_UserSubscription_UserSubscriptionId",
                        column: x => x.UserSubscriptionId,
                        principalTable: "UserSubscription",
                        principalColumn: "UserSubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationSession",
                columns: table => new
                {
                    ConsultationSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    VisitReason = table.Column<string>(type: "text", nullable: true),
                    CurrentSymptoms = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationSession", x => x.ConsultationSessionId);
                    table.ForeignKey(
                        name: "FK_ConsultationSession_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationSession_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ConsultationSession_MedicalDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MedicalDepartment",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationSession_MedicalFacility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "MedicalFacility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultationSession_SymptomAnalysisSession_SymptomAnalysisS~",
                        column: x => x.SymptomAnalysisSessionId,
                        principalTable: "SymptomAnalysisSession",
                        principalColumn: "SymptomAnalysisSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalVisit",
                columns: table => new
                {
                    VisitId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    DiagnosisNote = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalVisit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_MedicalVisit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalVisit_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalVisit_MedicalDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MedicalDepartment",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalVisit_MedicalFacility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "MedicalFacility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsultationSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationQuestion", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_ConsultationQuestion_ConsultationSession_ConsultationSessio~",
                        column: x => x.ConsultationSessionId,
                        principalTable: "ConsultationSession",
                        principalColumn: "ConsultationSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackReview",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackReview", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_FeedbackReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedbackReview_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedbackReview_MedicalFacility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "MedicalFacility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeedbackReview_MedicalVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "MedicalVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordType = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RecordDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_MedicalVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "MedicalVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentJourney",
                columns: table => new
                {
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DiagnosisSummary = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentJourney", x => x.TreatmentJourneyId);
                    table.ForeignKey(
                        name: "FK_TreatmentJourney_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreatmentJourney_MedicalVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "MedicalVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LabResult",
                columns: table => new
                {
                    LabResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabName = table.Column<string>(type: "text", nullable: true),
                    TestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OverallConclusion = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResult", x => x.LabResultId);
                    table.ForeignKey(
                        name: "FK_LabResult_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecordFile",
                columns: table => new
                {
                    MedicalRecordFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<string>(type: "text", nullable: true),
                    OriginalFileName = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecordFile", x => x.MedicalRecordFileId);
                    table.ForeignKey(
                        name: "FK_MedicalRecordFile_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIAnalysis",
                columns: table => new
                {
                    AIAnalysisId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceType = table.Column<string>(type: "text", nullable: true),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: true),
                    ResultSummary = table.Column<string>(type: "text", nullable: true),
                    DisclaimerText = table.Column<string>(type: "text", nullable: true),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIAnalysis", x => x.AIAnalysisId);
                    table.ForeignKey(
                        name: "FK_AIAnalysis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIAnalysis_TreatmentJourney_TreatmentJourneyId",
                        column: x => x.TreatmentJourneyId,
                        principalTable: "TreatmentJourney",
                        principalColumn: "TreatmentJourneyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugAnalysis",
                columns: table => new
                {
                    DrugAnalysisId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAnalysis", x => x.DrugAnalysisId);
                    table.ForeignKey(
                        name: "FK_DrugAnalysis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugAnalysis_TreatmentJourney_TreatmentJourneyId",
                        column: x => x.TreatmentJourneyId,
                        principalTable: "TreatmentJourney",
                        principalColumn: "TreatmentJourneyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecoveryPlan",
                columns: table => new
                {
                    RecoveryPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanName = table.Column<string>(type: "text", nullable: true),
                    DurationDays = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryPlan", x => x.RecoveryPlanId);
                    table.ForeignKey(
                        name: "FK_RecoveryPlan_TreatmentJourney_TreatmentJourneyId",
                        column: x => x.TreatmentJourneyId,
                        principalTable: "TreatmentJourney",
                        principalColumn: "TreatmentJourneyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMedication",
                columns: table => new
                {
                    UserMedicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: true),
                    DosageInstruction = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedication", x => x.UserMedicationId);
                    table.ForeignKey(
                        name: "FK_UserMedication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMedication_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMedication_TreatmentJourney_TreatmentJourneyId",
                        column: x => x.TreatmentJourneyId,
                        principalTable: "TreatmentJourney",
                        principalColumn: "TreatmentJourneyId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LabResultDetails",
                columns: table => new
                {
                    LabResultItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestName = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    NormalRangeMin = table.Column<double>(type: "double precision", nullable: true),
                    NormalRangeMax = table.Column<double>(type: "double precision", nullable: true),
                    Interpretation = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResultDetails", x => x.LabResultItemId);
                    table.ForeignKey(
                        name: "FK_LabResultDetails_LabResult_LabResultId",
                        column: x => x.LabResultId,
                        principalTable: "LabResult",
                        principalColumn: "LabResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugAnalysisResult",
                columns: table => new
                {
                    DrugAnalysisResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    DrugAnalysisId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    CombinationVerdict = table.Column<string>(type: "text", nullable: true),
                    InteractionDetail = table.Column<string>(type: "text", nullable: true),
                    DietaryInteraction = table.Column<string>(type: "text", nullable: true),
                    UsageOptimization = table.Column<string>(type: "text", nullable: true),
                    Precaution = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAnalysisResult", x => x.DrugAnalysisResultId);
                    table.ForeignKey(
                        name: "FK_DrugAnalysisResult_DrugAnalysis_DrugAnalysisId",
                        column: x => x.DrugAnalysisId,
                        principalTable: "DrugAnalysis",
                        principalColumn: "DrugAnalysisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugAnalysisResult_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AISystemConfig",
                columns: table => new
                {
                    ConfigId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomAnalysisSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicationScanId = table.Column<Guid>(type: "uuid", nullable: true),
                    VisitId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecoveryPlanId = table.Column<Guid>(type: "uuid", nullable: true),
                    DrugAnalysisId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConsultationSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemPrompt = table.Column<string>(type: "text", nullable: true),
                    ModelParams = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AISystemConfig", x => x.ConfigId);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_ConsultationSession_ConsultationSessionId",
                        column: x => x.ConsultationSessionId,
                        principalTable: "ConsultationSession",
                        principalColumn: "ConsultationSessionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_DrugAnalysis_DrugAnalysisId",
                        column: x => x.DrugAnalysisId,
                        principalTable: "DrugAnalysis",
                        principalColumn: "DrugAnalysisId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_MedicalVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "MedicalVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_MedicationScan_MedicationScanId",
                        column: x => x.MedicationScanId,
                        principalTable: "MedicationScan",
                        principalColumn: "MedicationScanId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_RecoveryPlan_RecoveryPlanId",
                        column: x => x.RecoveryPlanId,
                        principalTable: "RecoveryPlan",
                        principalColumn: "RecoveryPlanId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AISystemConfig_SymptomAnalysisSession_SymptomAnalysisSessio~",
                        column: x => x.SymptomAnalysisSessionId,
                        principalTable: "SymptomAnalysisSession",
                        principalColumn: "SymptomAnalysisSessionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentLog",
                columns: table => new
                {
                    TreatmentLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecoveryPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    DailyTaskJson = table.Column<string>(type: "text", nullable: true),
                    MedicationInstruction = table.Column<string>(type: "text", nullable: true),
                    IsMedicationTaken = table.Column<bool>(type: "boolean", nullable: false),
                    SymptomNote = table.Column<string>(type: "text", nullable: true),
                    Temperature = table.Column<double>(type: "double precision", nullable: true),
                    PainLevel = table.Column<int>(type: "integer", nullable: true),
                    AI_FeedbackNote = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentLog", x => x.TreatmentLogId);
                    table.ForeignKey(
                        name: "FK_TreatmentLog_RecoveryPlan_RecoveryPlanId",
                        column: x => x.RecoveryPlanId,
                        principalTable: "RecoveryPlan",
                        principalColumn: "RecoveryPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUpReminder",
                columns: table => new
                {
                    ReminderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentJourneyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreatmentLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderType = table.Column<string>(type: "text", nullable: true),
                    ReminderTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpReminder", x => x.ReminderId);
                    table.ForeignKey(
                        name: "FK_FollowUpReminder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUpReminder_TreatmentJourney_TreatmentJourneyId",
                        column: x => x.TreatmentJourneyId,
                        principalTable: "TreatmentJourney",
                        principalColumn: "TreatmentJourneyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUpReminder_TreatmentLog_TreatmentLogId",
                        column: x => x.TreatmentLogId,
                        principalTable: "TreatmentLog",
                        principalColumn: "TreatmentLogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Channel = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_FollowUpReminder_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "FollowUpReminder",
                        principalColumn: "ReminderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysis_TreatmentJourneyId",
                table: "AIAnalysis",
                column: "TreatmentJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysis_UserId",
                table: "AIAnalysis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_ConsultationSessionId",
                table: "AISystemConfig",
                column: "ConsultationSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_DrugAnalysisId",
                table: "AISystemConfig",
                column: "DrugAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_MedicationScanId",
                table: "AISystemConfig",
                column: "MedicationScanId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_RecoveryPlanId",
                table: "AISystemConfig",
                column: "RecoveryPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_SymptomAnalysisSessionId",
                table: "AISystemConfig",
                column: "SymptomAnalysisSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_TaskType",
                table: "AISystemConfig",
                column: "TaskType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AISystemConfig_VisitId",
                table: "AISystemConfig",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationQuestion_ConsultationSessionId",
                table: "ConsultationQuestion",
                column: "ConsultationSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationSession_DepartmentId",
                table: "ConsultationSession",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationSession_DoctorId",
                table: "ConsultationSession",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationSession_FacilityId",
                table: "ConsultationSession",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationSession_SymptomAnalysisSessionId",
                table: "ConsultationSession",
                column: "SymptomAnalysisSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationSession_UserId",
                table: "ConsultationSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRecommendation_DepartmentId",
                table: "DepartmentRecommendation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRecommendation_SymptomAnalysisSessionId",
                table: "DepartmentRecommendation",
                column: "SymptomAnalysisSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_FacilityDepartmentId",
                table: "Doctor",
                column: "FacilityDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAnalysis_TreatmentJourneyId",
                table: "DrugAnalysis",
                column: "TreatmentJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAnalysis_UserId",
                table: "DrugAnalysis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAnalysisResult_DrugAnalysisId",
                table: "DrugAnalysisResult",
                column: "DrugAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAnalysisResult_MedicineId",
                table: "DrugAnalysisResult",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityDepartment_DepartmentId",
                table: "FacilityDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityDepartment_FacilityId_DepartmentId",
                table: "FacilityDepartment",
                columns: new[] { "FacilityId", "DepartmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReview_DoctorId",
                table: "FeedbackReview",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReview_FacilityId",
                table: "FeedbackReview",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReview_UserId",
                table: "FeedbackReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReview_VisitId",
                table: "FeedbackReview",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminder_TreatmentJourneyId",
                table: "FollowUpReminder",
                column: "TreatmentJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminder_TreatmentLogId",
                table: "FollowUpReminder",
                column: "TreatmentLogId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminder_UserId",
                table: "FollowUpReminder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeChunk_KnowledgeDocumentId",
                table: "KnowledgeChunk",
                column: "KnowledgeDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeDocument_KnowledgeSourceId",
                table: "KnowledgeDocument",
                column: "KnowledgeSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResult_MedicalRecordId",
                table: "LabResult",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResultDetails_LabResultId",
                table: "LabResultDetails",
                column: "LabResultId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_UserId",
                table: "MedicalRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_VisitId",
                table: "MedicalRecord",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecordFile_MedicalRecordId",
                table: "MedicalRecordFile",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisit_DepartmentId",
                table: "MedicalVisit",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisit_DoctorId",
                table: "MedicalVisit",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisit_FacilityId",
                table: "MedicalVisit",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisit_UserId",
                table: "MedicalVisit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationScan_UserId",
                table: "MedicationScan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationScanResult_MedicationScanId",
                table: "MedicationScanResult",
                column: "MedicationScanId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationScanResult_MedicineId",
                table: "MedicationScanResult",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ReminderId",
                table: "Notification",
                column: "ReminderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfile_UserId",
                table: "PatientProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_UserId",
                table: "PaymentTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_UserSubscriptionId",
                table: "PaymentTransaction",
                column: "UserSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPlan_TreatmentJourneyId",
                table: "RecoveryPlan",
                column: "TreatmentJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSymptom_SymptomAnalysisSessionId",
                table: "SessionSymptom",
                column: "SymptomAnalysisSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomAnalysisSession_UserId",
                table: "SymptomAnalysisSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentJourney_UserId",
                table: "TreatmentJourney",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentJourney_VisitId",
                table: "TreatmentJourney",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentLog_RecoveryPlanId",
                table: "TreatmentLog",
                column: "RecoveryPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedication_MedicineId",
                table: "UserMedication",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedication_TreatmentJourneyId",
                table: "UserMedication",
                column: "TreatmentJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedication_UserId",
                table: "UserMedication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_PlanId",
                table: "UserSubscription",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_UserId",
                table: "UserSubscription",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIAnalysis");

            migrationBuilder.DropTable(
                name: "AISystemConfig");

            migrationBuilder.DropTable(
                name: "ConsultationQuestion");

            migrationBuilder.DropTable(
                name: "DepartmentRecommendation");

            migrationBuilder.DropTable(
                name: "DrugAnalysisResult");

            migrationBuilder.DropTable(
                name: "FeedbackReview");

            migrationBuilder.DropTable(
                name: "KnowledgeChunk");

            migrationBuilder.DropTable(
                name: "LabResultDetails");

            migrationBuilder.DropTable(
                name: "MedicalRecordFile");

            migrationBuilder.DropTable(
                name: "MedicationScanResult");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PatientProfile");

            migrationBuilder.DropTable(
                name: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "SessionSymptom");

            migrationBuilder.DropTable(
                name: "UserMedication");

            migrationBuilder.DropTable(
                name: "ConsultationSession");

            migrationBuilder.DropTable(
                name: "DrugAnalysis");

            migrationBuilder.DropTable(
                name: "KnowledgeDocument");

            migrationBuilder.DropTable(
                name: "LabResult");

            migrationBuilder.DropTable(
                name: "MedicationScan");

            migrationBuilder.DropTable(
                name: "FollowUpReminder");

            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "SymptomAnalysisSession");

            migrationBuilder.DropTable(
                name: "KnowledgeSource");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "TreatmentLog");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "RecoveryPlan");

            migrationBuilder.DropTable(
                name: "TreatmentJourney");

            migrationBuilder.DropTable(
                name: "MedicalVisit");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "FacilityDepartment");

            migrationBuilder.DropTable(
                name: "MedicalDepartment");

            migrationBuilder.DropTable(
                name: "MedicalFacility");
        }
    }
}
