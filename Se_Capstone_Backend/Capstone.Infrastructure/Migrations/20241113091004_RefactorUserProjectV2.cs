using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUserProjectV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leave_logs");

            migrationBuilder.DropTable(
                name: "logEntries");

            migrationBuilder.DropTable(
                name: "projectUsers");

            migrationBuilder.CreateTable(
                name: "userProjects",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    projectId = table.Column<Guid>(type: "uuid", nullable: false),
                    positionId = table.Column<Guid>(type: "uuid", nullable: false),
                    isProjectConfigurator = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    isIssueConfigurator = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true),
                    updatedBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userProjects", x => new { x.userId, x.projectId });
                    table.ForeignKey(
                        name: "FK_userProjects_positions_positionId",
                        column: x => x.positionId,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_userProjects_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userProjects_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_positionId",
                table: "userProjects",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_projectId",
                table: "userProjects",
                column: "projectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userProjects");

            migrationBuilder.CreateTable(
                name: "leave_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true),
                    endTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isApprove = table.Column<bool>(type: "boolean", nullable: false),
                    isChecked = table.Column<bool>(type: "boolean", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isFullDay = table.Column<bool>(type: "boolean", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false),
                    reason = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    startTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_leave_logs_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logEntries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true),
                    errorMessage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    isChecked = table.Column<bool>(type: "boolean", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logEntries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projectUsers",
                columns: table => new
                {
                    projectId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectUsers", x => new { x.projectId, x.userId });
                    table.ForeignKey(
                        name: "FK_projectUsers_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectUsers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_leave_logs_userId",
                table: "leave_logs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_projectUsers_userId",
                table: "projectUsers",
                column: "userId");
        }
    }
}
