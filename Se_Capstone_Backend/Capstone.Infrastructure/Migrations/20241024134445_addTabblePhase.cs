using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTabblePhase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_projects_projectId",
                table: "issues");

            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "issues",
                newName: "phaseId");

            migrationBuilder.RenameIndex(
                name: "IX_issues_projectId",
                table: "issues",
                newName: "IX_issues_phaseId");

            migrationBuilder.CreateTable(
                name: "phases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    expectedStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    expectedEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    actualStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    actualEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    projectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phases", x => x.id);
                    table.ForeignKey(
                        name: "FK_phases_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_phases_projectId",
                table: "phases",
                column: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_phases_phaseId",
                table: "issues",
                column: "phaseId",
                principalTable: "phases",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_phases_phaseId",
                table: "issues");

            migrationBuilder.DropTable(
                name: "phases");

            migrationBuilder.RenameColumn(
                name: "phaseId",
                table: "issues",
                newName: "projectId");

            migrationBuilder.RenameIndex(
                name: "IX_issues_phaseId",
                table: "issues",
                newName: "IX_issues_projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_projects_projectId",
                table: "issues",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
