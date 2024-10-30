using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorIssuesTableV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_labels_labelId",
                table: "issues");


            migrationBuilder.RenameColumn(
                name: "assignedId",
                table: "issues",
                newName: "assignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_issues_projectId",
                table: "issues",
                newName: "IX_issues_phaseId");

            migrationBuilder.AlterColumn<Guid>(
                name: "labelId",
                table: "issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "assignedById",
                table: "issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "phases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    expectedStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expectedEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                name: "IX_issues_assignedById",
                table: "issues",
                column: "assignedById");

            migrationBuilder.CreateIndex(
                name: "IX_issues_assignedToId",
                table: "issues",
                column: "assignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_phases_projectId",
                table: "phases",
                column: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_labels_labelId",
                table: "issues",
                column: "labelId",
                principalTable: "labels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_issues_phases_phaseId",
                table: "issues",
                column: "phaseId",
                principalTable: "phases",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_issues_users_assignedById",
                table: "issues",
                column: "assignedById",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_issues_users_assignedToId",
                table: "issues",
                column: "assignedToId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_labels_labelId",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_phases_phaseId",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_users_assignedById",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_users_assignedToId",
                table: "issues");

            migrationBuilder.DropTable(
                name: "phases");

            migrationBuilder.DropIndex(
                name: "IX_issues_assignedById",
                table: "issues");

            migrationBuilder.DropIndex(
                name: "IX_issues_assignedToId",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "assignedById",
                table: "issues");

            migrationBuilder.RenameColumn(
                name: "phaseId",
                table: "issues",
                newName: "projectId");

            migrationBuilder.RenameColumn(
                name: "assignedToId",
                table: "issues",
                newName: "assignedId");

            migrationBuilder.RenameIndex(
                name: "IX_issues_phaseId",
                table: "issues",
                newName: "IX_issues_projectId");

            migrationBuilder.AlterColumn<Guid>(
                name: "labelId",
                table: "issues",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_labels_labelId",
                table: "issues",
                column: "labelId",
                principalTable: "labels",
                principalColumn: "id");

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
