using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorIssuesTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assignedId",
                table: "issues",
                newName: "assignedToId");

            migrationBuilder.AddColumn<Guid>(
                name: "assignedById",
                table: "issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_issues_assignedById",
                table: "issues",
                column: "assignedById");

            migrationBuilder.CreateIndex(
                name: "IX_issues_assignedToId",
                table: "issues",
                column: "assignedToId");

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
                name: "FK_issues_users_assignedById",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_users_assignedToId",
                table: "issues");

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
                name: "assignedToId",
                table: "issues",
                newName: "assignedId");
        }
    }
}
