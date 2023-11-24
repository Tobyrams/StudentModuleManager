using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POE_TEST_ONE.Migrations
{
    /// <inheritdoc />
    public partial class AddModulesTableToPoeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Module_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Num_credits = table.Column<int>(type: "int", nullable: false),
                    Class_hours_per_week = table.Column<int>(type: "int", nullable: false),
                    Num_weeks_in_semester = table.Column<int>(type: "int", nullable: false),
                    Semester_start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Studied_on_certin_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours_Studied_On_CertinDate = table.Column<int>(type: "int", nullable: false),
                    Self_Study_hours = table.Column<int>(type: "int", nullable: false),
                    Remaining_study_hours = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
