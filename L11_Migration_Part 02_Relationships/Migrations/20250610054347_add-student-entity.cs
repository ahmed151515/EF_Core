﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L11_Migration_Part_02_Relationships.Migrations
{
	/// <inheritdoc />
	public partial class addstudententity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Students",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false),
					FName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
					LName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Students", x => x.Id);
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
						name: "FK_Enrollmens_Sections_SectionId",
						column: x => x.SectionId,
						principalTable: "Sections",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Enrollmens_Students_StudentId",
						column: x => x.StudentId,
						principalTable: "Students",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "Students",
				columns: new[] { "Id", "FName", "LName" },
				values: new object[,]
				{
					{ 1, "Fatima", "Ali" },
					{ 2, "Noor", "Saleh" },
					{ 3, "Omar", "Youssef" },
					{ 4, "Huda", "Ahmed" },
					{ 5, "Amira", "Tariq" },
					{ 6, "Zainab", "Ismail" },
					{ 7, "Yousef", "Farid" },
					{ 8, "Layla", "Mustafa" },
					{ 9, "Mohammed", "Adel" },
					{ 10, "Samira", "Nabil" }
				});

			migrationBuilder.InsertData(
				table: "Enrollmens",
				columns: new[] { "SectionId", "StudentId" },
				values: new object[,]
				{
					{ 6, 1 },
					{ 6, 2 },
					{ 7, 3 },
					{ 7, 4 },
					{ 8, 5 },
					{ 8, 6 },
					{ 9, 7 },
					{ 9, 8 },
					{ 10, 9 },
					{ 10, 10 }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Enrollmens_StudentId",
				table: "Enrollmens",
				column: "StudentId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Enrollmens");

			migrationBuilder.DropTable(
				name: "Students");
		}
	}
}
