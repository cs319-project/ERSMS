using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sender = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateOfApproval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExemptedCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BilkentCredits = table.Column<int>(type: "integer", nullable: false),
                    ECTS = table.Column<double>(type: "double precision", nullable: false),
                    CourseCode = table.Column<string>(type: "text", nullable: true),
                    CourseName = table.Column<string>(type: "text", nullable: true),
                    CourseType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExemptedCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoggedTransferredCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LogTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedTransferredCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    read = table.Column<bool>(type: "boolean", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacedStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true),
                    CGPA = table.Column<double>(type: "double precision", nullable: false),
                    ExchangeScore = table.Column<double>(type: "double precision", nullable: false),
                    PreferredSemester_AcademicYear = table.Column<string>(type: "text", nullable: true),
                    PreferredSemester_Semester = table.Column<int>(type: "integer", nullable: true),
                    PreferredSchools = table.Column<string>(type: "text", nullable: true),
                    ExchangeSchool = table.Column<string>(type: "text", nullable: true),
                    IsPlaced = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedStudents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacementTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    ExcelFile = table.Column<byte[]>(type: "bytea", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DomainUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainUsers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoggedEquivalentCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExemptedCourseId = table.Column<Guid>(type: "uuid", nullable: true),
                    HostCourseCode = table.Column<string>(type: "text", nullable: true),
                    HostCourseName = table.Column<string>(type: "text", nullable: true),
                    HostCourseECTS = table.Column<double>(type: "double precision", nullable: false),
                    HostSchool = table.Column<string>(type: "text", nullable: true),
                    LogTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedEquivalentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggedEquivalentCourses_ExemptedCourse_ExemptedCourseId",
                        column: x => x.ExemptedCourseId,
                        principalTable: "ExemptedCourse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseCoordinatorInstructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true),
                    Course_CourseCode = table.Column<string>(type: "text", nullable: true),
                    Course_CourseName = table.Column<string>(type: "text", nullable: true),
                    IsCourseCoordinator = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCoordinatorInstructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCoordinatorInstructors_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeanDepartmentChairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true),
                    IsDean = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeanDepartmentChairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeanDepartmentChairs_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeCoordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeCoordinators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeCoordinators_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderUsername = table.Column<string>(type: "text", nullable: true),
                    RecipientUsername = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DomainUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DomainUserId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_DomainUsers_DomainUserId",
                        column: x => x.DomainUserId,
                        principalTable: "DomainUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_DomainUsers_DomainUserId1",
                        column: x => x.DomainUserId1,
                        principalTable: "DomainUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OISEPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OISEPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OISEPs_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntranceYear = table.Column<int>(type: "integer", nullable: false),
                    DepartmentName = table.Column<int>(type: "integer", nullable: true),
                    FacultyName = table.Column<int>(type: "integer", nullable: true),
                    CGPA = table.Column<double>(type: "double precision", nullable: false),
                    ExchangeScore = table.Column<double>(type: "double precision", nullable: false),
                    PreferredSemester_AcademicYear = table.Column<string>(type: "text", nullable: true),
                    PreferredSemester_Semester = table.Column<int>(type: "integer", nullable: true),
                    PreferredSchools = table.Column<string>(type: "text", nullable: true),
                    ExchangeSchool = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_DomainUsers_Id",
                        column: x => x.Id,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTEForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IDNumber = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<int>(type: "integer", nullable: false),
                    HostUniversityName = table.Column<string>(type: "text", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PDF = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    ChairApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeanApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    FacultyOfAdministrationBoardApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToDoItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTEForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTEForms_Approval_ChairApprovalId",
                        column: x => x.ChairApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForms_Approval_DeanApprovalId",
                        column: x => x.DeanApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForms_Approval_ExchangeCoordinatorApprovalId",
                        column: x => x.ExchangeCoordinatorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForms_Approval_FacultyOfAdministrationBoardApprovalId",
                        column: x => x.FacultyOfAdministrationBoardApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquivalenceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    HostCourseName = table.Column<string>(type: "text", nullable: true),
                    HostCourseCode = table.Column<string>(type: "text", nullable: true),
                    HostCourseECTS = table.Column<double>(type: "double precision", nullable: false),
                    HostUniversityName = table.Column<string>(type: "text", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Syllabus = table.Column<byte[]>(type: "bytea", nullable: true),
                    ExemptedCourseId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstructorApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    StudentId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalenceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquivalenceRequests_Approval_InstructorApprovalId",
                        column: x => x.InstructorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquivalenceRequests_ExemptedCourse_ExemptedCourseId",
                        column: x => x.ExemptedCourseId,
                        principalTable: "ExemptedCourse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquivalenceRequests_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreApprovalForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IDNumber = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<int>(type: "integer", nullable: false),
                    HostUniversityName = table.Column<string>(type: "text", nullable: false),
                    AcademicYear = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    PDF = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    SubmissionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    FacultyAdministrationBoardApprovalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToDoItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreApprovalForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreApprovalForms_Approval_ExchangeCoordinatorApprovalId",
                        column: x => x.ExchangeCoordinatorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreApprovalForms_Approval_FacultyAdministrationBoardApprova~",
                        column: x => x.FacultyAdministrationBoardApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreApprovalForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students_Minors",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<int>(type: "integer", nullable: false),
                    FacultyName = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students_Minors", x => new { x.StudentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Students_Minors_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CascadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    IsStarred = table.Column<bool>(type: "boolean", nullable: false),
                    ExchangeCoordinatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ExchangeCoordinators_ExchangeCoordinatorId",
                        column: x => x.ExchangeCoordinatorId,
                        principalTable: "ExchangeCoordinators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ToDoItems_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferredCourseGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExemptedCourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CTEFormId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoggedTransferredCourseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferredCourseGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferredCourseGroup_CTEForms_CTEFormId",
                        column: x => x.CTEFormId,
                        principalTable: "CTEForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferredCourseGroup_ExemptedCourse_ExemptedCourseId",
                        column: x => x.ExemptedCourseId,
                        principalTable: "ExemptedCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferredCourseGroup_LoggedTransferredCourses_LoggedTrans~",
                        column: x => x.LoggedTransferredCourseId,
                        principalTable: "LoggedTransferredCourses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestedCourseGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedExemptedCourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreApprovalFormId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedCourseGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedCourseGroup_ExemptedCourse_RequestedExemptedCourse~",
                        column: x => x.RequestedExemptedCourseId,
                        principalTable: "ExemptedCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestedCourseGroup_PreApprovalForms_PreApprovalFormId",
                        column: x => x.PreApprovalFormId,
                        principalTable: "PreApprovalForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferredCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseCode = table.Column<string>(type: "text", nullable: false),
                    CourseName = table.Column<string>(type: "text", nullable: false),
                    ECTS = table.Column<double>(type: "double precision", nullable: false),
                    Grade = table.Column<string>(type: "text", nullable: false),
                    TransferredCourseGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferredCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferredCourse_TransferredCourseGroup_TransferredCourseG~",
                        column: x => x.TransferredCourseGroupId,
                        principalTable: "TransferredCourseGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestedCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseCode = table.Column<string>(type: "text", nullable: false),
                    CourseName = table.Column<string>(type: "text", nullable: false),
                    ECTS = table.Column<double>(type: "double precision", nullable: false),
                    RequestedCourseGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedCourse_RequestedCourseGroup_RequestedCourseGroupId",
                        column: x => x.RequestedCourseGroupId,
                        principalTable: "RequestedCourseGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTEForms_ChairApprovalId",
                table: "CTEForms",
                column: "ChairApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForms_DeanApprovalId",
                table: "CTEForms",
                column: "DeanApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForms_ExchangeCoordinatorApprovalId",
                table: "CTEForms",
                column: "ExchangeCoordinatorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForms_FacultyOfAdministrationBoardApprovalId",
                table: "CTEForms",
                column: "FacultyOfAdministrationBoardApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForms_StudentId",
                table: "CTEForms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainUsers_AppUserId",
                table: "DomainUsers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquivalenceRequests_ExemptedCourseId",
                table: "EquivalenceRequests",
                column: "ExemptedCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EquivalenceRequests_InstructorApprovalId",
                table: "EquivalenceRequests",
                column: "InstructorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_EquivalenceRequests_StudentId1",
                table: "EquivalenceRequests",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoggedEquivalentCourses_ExemptedCourseId",
                table: "LoggedEquivalentCourses",
                column: "ExemptedCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DomainUserId",
                table: "Messages",
                column: "DomainUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DomainUserId1",
                table: "Messages",
                column: "DomainUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PreApprovalForms_ExchangeCoordinatorApprovalId",
                table: "PreApprovalForms",
                column: "ExchangeCoordinatorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_PreApprovalForms_FacultyAdministrationBoardApprovalId",
                table: "PreApprovalForms",
                column: "FacultyAdministrationBoardApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_PreApprovalForms_StudentId",
                table: "PreApprovalForms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedCourse_RequestedCourseGroupId",
                table: "RequestedCourse",
                column: "RequestedCourseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedCourseGroup_PreApprovalFormId",
                table: "RequestedCourseGroup",
                column: "PreApprovalFormId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedCourseGroup_RequestedExemptedCourseId",
                table: "RequestedCourseGroup",
                column: "RequestedExemptedCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ExchangeCoordinatorId",
                table: "ToDoItems",
                column: "ExchangeCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_StudentId",
                table: "ToDoItems",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredCourse_TransferredCourseGroupId",
                table: "TransferredCourse",
                column: "TransferredCourseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredCourseGroup_CTEFormId",
                table: "TransferredCourseGroup",
                column: "CTEFormId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredCourseGroup_ExemptedCourseId",
                table: "TransferredCourseGroup",
                column: "ExemptedCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferredCourseGroup_LoggedTransferredCourseId",
                table: "TransferredCourseGroup",
                column: "LoggedTransferredCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CourseCoordinatorInstructors");

            migrationBuilder.DropTable(
                name: "DeanDepartmentChairs");

            migrationBuilder.DropTable(
                name: "EquivalenceRequests");

            migrationBuilder.DropTable(
                name: "LoggedEquivalentCourses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OISEPs");

            migrationBuilder.DropTable(
                name: "PlacedStudents");

            migrationBuilder.DropTable(
                name: "PlacementTables");

            migrationBuilder.DropTable(
                name: "RequestedCourse");

            migrationBuilder.DropTable(
                name: "Students_Minors");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "TransferredCourse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RequestedCourseGroup");

            migrationBuilder.DropTable(
                name: "ExchangeCoordinators");

            migrationBuilder.DropTable(
                name: "TransferredCourseGroup");

            migrationBuilder.DropTable(
                name: "PreApprovalForms");

            migrationBuilder.DropTable(
                name: "CTEForms");

            migrationBuilder.DropTable(
                name: "ExemptedCourse");

            migrationBuilder.DropTable(
                name: "LoggedTransferredCourses");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "DomainUsers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
