using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStaffTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicants_staffs_staffId",
                table: "applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_attendances_staffs_staffId",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_staffs_assignedId",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_leave_logs_staffs_staffId",
                table: "leave_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_staffs_leadId",
                table: "projects");

            migrationBuilder.DropTable(
                name: "projectStaffs");

            migrationBuilder.DropTable(
                name: "staffs");

            migrationBuilder.DropIndex(
                name: "IX_issues_assignedId",
                table: "issues");

            migrationBuilder.RenameColumn(
                name: "staffId",
                table: "leave_logs",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_leave_logs_staffId",
                table: "leave_logs",
                newName: "IX_leave_logs_userId");

            migrationBuilder.RenameColumn(
                name: "staffId",
                table: "attendances",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_staffId",
                table: "attendances",
                newName: "IX_attendances_userId");

            migrationBuilder.RenameColumn(
                name: "staffId",
                table: "applicants",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_applicants_staffId",
                table: "applicants",
                newName: "IX_applicants_userId");

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
                name: "IX_projectUsers_userId",
                table: "projectUsers",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicants_users_userId",
                table: "applicants",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_users_userId",
                table: "attendances",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_leave_logs_users_userId",
                table: "leave_logs",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_leadId",
                table: "projects",
                column: "leadId",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicants_users_userId",
                table: "applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_attendances_users_userId",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_leave_logs_users_userId",
                table: "leave_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_leadId",
                table: "projects");

            migrationBuilder.DropTable(
                name: "projectUsers");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "leave_logs",
                newName: "staffId");

            migrationBuilder.RenameIndex(
                name: "IX_leave_logs_userId",
                table: "leave_logs",
                newName: "IX_leave_logs_staffId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "attendances",
                newName: "staffId");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_userId",
                table: "attendances",
                newName: "IX_attendances_staffId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "applicants",
                newName: "staffId");

            migrationBuilder.RenameIndex(
                name: "IX_applicants_userId",
                table: "applicants",
                newName: "IX_applicants_staffId");

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updateBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffs", x => x.id);
                    table.ForeignKey(
                        name: "FK_staffs_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projectStaffs",
                columns: table => new
                {
                    projectId = table.Column<Guid>(type: "uuid", nullable: false),
                    staffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectStaffs", x => new { x.projectId, x.staffId });
                    table.ForeignKey(
                        name: "FK_projectStaffs_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectStaffs_staffs_staffId",
                        column: x => x.staffId,
                        principalTable: "staffs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_issues_assignedId",
                table: "issues",
                column: "assignedId");

            migrationBuilder.CreateIndex(
                name: "IX_projectStaffs_staffId",
                table: "projectStaffs",
                column: "staffId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicants_staffs_staffId",
                table: "applicants",
                column: "staffId",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_staffs_staffId",
                table: "attendances",
                column: "staffId",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_issues_staffs_assignedId",
                table: "issues",
                column: "assignedId",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_leave_logs_staffs_staffId",
                table: "leave_logs",
                column: "staffId",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_staffs_leadId",
                table: "projects",
                column: "leadId",
                principalTable: "staffs",
                principalColumn: "id");
        }
    }
}
