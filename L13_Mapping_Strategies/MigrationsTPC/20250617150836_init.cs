using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L13_Mapping_Strategies.MigrationsTPC
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OfficeLocation = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OfficeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfGraduation = table.Column<int>(type: "int", nullable: true),
                    IsIntern = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    SUN = table.Column<bool>(type: "bit", nullable: false),
                    MON = table.Column<bool>(type: "bit", nullable: false),
                    TUE = table.Column<bool>(type: "bit", nullable: false),
                    WED = table.Column<bool>(type: "bit", nullable: false),
                    THU = table.Column<bool>(type: "bit", nullable: false),
                    FRI = table.Column<bool>(type: "bit", nullable: false),
                    SAT = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoices_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrueAndFalseQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueAndFalseQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrueAndFalseQuizzes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SectionName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sections_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollmens",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollmens", x => new { x.SectionId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Enrollmens_Participants_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollmens_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollmens_StudentId",
                table: "Enrollmens",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_OfficeId",
                table: "Instructors",
                column: "OfficeId",
                unique: true,
                filter: "[OfficeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoices_CourseId",
                table: "MultipleChoices",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_InstructorId",
                table: "Sections",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ScheduleId",
                table: "Sections",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueAndFalseQuizzes_CourseId",
                table: "TrueAndFalseQuizzes",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollmens");

            migrationBuilder.DropTable(
                name: "MultipleChoices");

            migrationBuilder.DropTable(
                name: "TrueAndFalseQuizzes");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
