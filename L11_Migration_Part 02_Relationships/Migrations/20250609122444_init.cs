using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L11_Migration_Part_02_Relationships.Migrations
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
				name: "Instructors",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false),
					FName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
					LName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Instructors", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Courses",
				columns: new[] { "Id", "CourseName", "Price" },
				values: new object[,]
				{
					{ 1, "Mathematics", 1000m },
					{ 2, "Physics", 2000m },
					{ 3, "Chemistry", 1500m },
					{ 4, "Biology", 1200m },
					{ 5, "Computer Science", 3000m }
				});

			migrationBuilder.InsertData(
				table: "Instructors",
				columns: new[] { "Id", "FName", "LName" },
				values: new object[,]
				{
					{ 1, "Ahmed", "Abdullah" },
					{ 2, "Yasmeen", "Yasmeen" },
					{ 3, "Khalid", "Hassan" },
					{ 4, "Nadia", "Ali" },
					{ 5, "Ahmed", "Abdullah" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Courses");

			migrationBuilder.DropTable(
				name: "Instructors");
		}
	}
}
