using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfApproval = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExemptedCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: true),
                    CourseName = table.Column<string>(type: "TEXT", nullable: true),
                    CourseType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExemptedCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacedStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true),
                    CGPA = table.Column<double>(type: "REAL", nullable: false),
                    ExchangeScore = table.Column<double>(type: "REAL", nullable: false),
                    PreferredSemester_AcademicYear = table.Column<string>(type: "TEXT", nullable: true),
                    PreferredSemester_Semester = table.Column<int>(type: "INTEGER", nullable: true),
                    PreferredSchools = table.Column<string>(type: "TEXT", nullable: true),
                    ExchangeSchool = table.Column<string>(type: "TEXT", nullable: true),
                    IsPlaced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedStudents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacementTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    ExcelFile = table.Column<byte[]>(type: "BLOB", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    AppUserId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true),
                    Course_CourseCode = table.Column<string>(type: "TEXT", nullable: true),
                    Course_CourseName = table.Column<string>(type: "TEXT", nullable: true),
                    IsCourseCoordinator = table.Column<bool>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDean = table.Column<bool>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "OISEPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntranceYear = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: true),
                    CGPA = table.Column<double>(type: "REAL", nullable: false),
                    ExchangeScore = table.Column<double>(type: "REAL", nullable: false),
                    PreferredSemester_AcademicYear = table.Column<string>(type: "TEXT", nullable: true),
                    PreferredSemester_Semester = table.Column<int>(type: "INTEGER", nullable: true),
                    PreferredSchools = table.Column<string>(type: "TEXT", nullable: true),
                    ExchangeSchool = table.Column<string>(type: "TEXT", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    IDNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<int>(type: "INTEGER", nullable: false),
                    HostUniversityName = table.Column<string>(type: "TEXT", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChairApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeanApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FacultyOfAdministrationBoardApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ToDoItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                name: "EquivalanceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<string>(type: "TEXT", nullable: false),
                    HostCourseName = table.Column<string>(type: "TEXT", nullable: true),
                    Syllabus = table.Column<byte[]>(type: "BLOB", nullable: true),
                    ExemptedCourseId = table.Column<Guid>(type: "TEXT", nullable: true),
                    InstructorApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "TEXT", nullable: true),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    StudentId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquivalanceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquivalanceRequests_Approval_InstructorApprovalId",
                        column: x => x.InstructorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquivalanceRequests_ExemptedCourse_ExemptedCourseId",
                        column: x => x.ExemptedCourseId,
                        principalTable: "ExemptedCourse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquivalanceRequests_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreApprovalForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    IDNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<int>(type: "INTEGER", nullable: false),
                    HostUniversityName = table.Column<string>(type: "TEXT", nullable: false),
                    AcademicYear = table.Column<string>(type: "TEXT", nullable: false),
                    Semester = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FacultyAdministrationBoardApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ToDoItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                        name: "FK_PreApprovalForms_Approval_FacultyAdministrationBoardApprovalId",
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
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: false),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CascadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsComplete = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStarred = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExchangeCoordinatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExemptedCourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CTEFormId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "RequestedCourseGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RequestedExemptedCourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreApprovalFormId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedCourseGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedCourseGroup_ExemptedCourse_RequestedExemptedCourseId",
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: false),
                    CourseName = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false),
                    TransferredCourseGroupId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferredCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferredCourse_TransferredCourseGroup_TransferredCourseGroupId",
                        column: x => x.TransferredCourseGroupId,
                        principalTable: "TransferredCourseGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestedCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: false),
                    CourseName = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestedCourseGroupId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                name: "IX_EquivalanceRequests_ExemptedCourseId",
                table: "EquivalanceRequests",
                column: "ExemptedCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EquivalanceRequests_InstructorApprovalId",
                table: "EquivalanceRequests",
                column: "InstructorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_EquivalanceRequests_StudentId1",
                table: "EquivalanceRequests",
                column: "StudentId1");

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
                name: "EquivalanceRequests");

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
