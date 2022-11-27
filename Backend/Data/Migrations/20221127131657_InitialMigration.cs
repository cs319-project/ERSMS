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
                name: "RequestedExemptedCourse",
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
                    table.PrimaryKey("PK_RequestedExemptedCourse", x => x.Id);
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
                name: "ExchangeCoordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Department_DepartmentName = table.Column<int>(type: "INTEGER", nullable: true),
                    Department_FacultyName = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntranceYear = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "CTEForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectStudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChairApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeanApprovalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTEForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTEForm_Approval_ChairApprovalId",
                        column: x => x.ChairApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForm_Approval_DeanApprovalId",
                        column: x => x.DeanApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForm_Approval_ExchangeCoordinatorApprovalId",
                        column: x => x.ExchangeCoordinatorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTEForm_Students_SubjectStudentId",
                        column: x => x.SubjectStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExemptionRequestForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectStudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CourseCodeHost = table.Column<string>(type: "TEXT", nullable: true),
                    CourseCodeBilkent = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExemptionRequestForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExemptionRequestForm_Students_SubjectStudentId",
                        column: x => x.SubjectStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreApprovalForm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectStudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExchangeCoordinatorApprovalId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreApprovalForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreApprovalForm_Approval_ExchangeCoordinatorApprovalId",
                        column: x => x.ExchangeCoordinatorApprovalId,
                        principalTable: "Approval",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreApprovalForm_Students_SubjectStudentId",
                        column: x => x.SubjectStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students_Majors",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentName = table.Column<int>(type: "INTEGER", nullable: false),
                    FacultyName = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students_Majors", x => new { x.StudentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Students_Majors_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_TransferredCourseGroup_CTEForm_CTEFormId",
                        column: x => x.CTEFormId,
                        principalTable: "CTEForm",
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
                        name: "FK_RequestedCourseGroup_PreApprovalForm_PreApprovalFormId",
                        column: x => x.PreApprovalFormId,
                        principalTable: "PreApprovalForm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestedCourseGroup_RequestedExemptedCourse_RequestedExemptedCourseId",
                        column: x => x.RequestedExemptedCourseId,
                        principalTable: "RequestedExemptedCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CTEForm_ChairApprovalId",
                table: "CTEForm",
                column: "ChairApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForm_DeanApprovalId",
                table: "CTEForm",
                column: "DeanApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForm_ExchangeCoordinatorApprovalId",
                table: "CTEForm",
                column: "ExchangeCoordinatorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_CTEForm_SubjectStudentId",
                table: "CTEForm",
                column: "SubjectStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainUsers_AppUserId",
                table: "DomainUsers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExemptionRequestForm_SubjectStudentId",
                table: "ExemptionRequestForm",
                column: "SubjectStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PreApprovalForm_ExchangeCoordinatorApprovalId",
                table: "PreApprovalForm",
                column: "ExchangeCoordinatorApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_PreApprovalForm_SubjectStudentId",
                table: "PreApprovalForm",
                column: "SubjectStudentId");

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
                name: "ExchangeCoordinators");

            migrationBuilder.DropTable(
                name: "ExemptionRequestForm");

            migrationBuilder.DropTable(
                name: "RequestedCourse");

            migrationBuilder.DropTable(
                name: "Students_Majors");

            migrationBuilder.DropTable(
                name: "Students_Minors");

            migrationBuilder.DropTable(
                name: "TransferredCourse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RequestedCourseGroup");

            migrationBuilder.DropTable(
                name: "TransferredCourseGroup");

            migrationBuilder.DropTable(
                name: "PreApprovalForm");

            migrationBuilder.DropTable(
                name: "RequestedExemptedCourse");

            migrationBuilder.DropTable(
                name: "CTEForm");

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
