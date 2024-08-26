using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteSprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projectSprints");

            migrationBuilder.DropTable(
                name: "sprintIssues");

            migrationBuilder.DropTable(
                name: "sprints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sprints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sprints", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projectSprints",
                columns: table => new
                {
                    projectId = table.Column<Guid>(type: "uuid", nullable: false),
                    sprintId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectSprints", x => new { x.projectId, x.sprintId });
                    table.ForeignKey(
                        name: "FK_projectSprints_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectSprints_sprints_sprintId",
                        column: x => x.sprintId,
                        principalTable: "sprints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sprintIssues",
                columns: table => new
                {
                    issueId = table.Column<Guid>(type: "uuid", nullable: false),
                    sprintId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sprintIssues", x => new { x.issueId, x.sprintId });
                    table.ForeignKey(
                        name: "FK_sprintIssues_issues_issueId",
                        column: x => x.issueId,
                        principalTable: "issues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sprintIssues_sprints_sprintId",
                        column: x => x.sprintId,
                        principalTable: "sprints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projectSprints_sprintId",
                table: "projectSprints",
                column: "sprintId");

            migrationBuilder.CreateIndex(
                name: "IX_sprintIssues_sprintId",
                table: "sprintIssues",
                column: "sprintId");
        }
    }
}
