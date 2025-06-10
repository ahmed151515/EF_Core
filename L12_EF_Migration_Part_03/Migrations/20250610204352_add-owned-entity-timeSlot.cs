using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L12_EF_Migration_Part_03.Migrations
{
    /// <inheritdoc />
    public partial class addownedentitytimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Sections",
                type: "Time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Sections",
                type: "Time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Sections",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "Time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Sections",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "Time");
        }
    }
}
