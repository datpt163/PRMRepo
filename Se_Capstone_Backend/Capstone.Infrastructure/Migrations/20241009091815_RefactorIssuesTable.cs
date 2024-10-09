using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorIssuesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "issues",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "index",
                table: "issues",
                type: "integer",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "lastUpdateById",
                table: "issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "parentIssueId",
                table: "issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    issueId = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    createAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_issues_issueId",
                        column: x => x.issueId,
                        principalTable: "issues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_issues_lastUpdateById",
                table: "issues",
                column: "lastUpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_issues_parentIssueId",
                table: "issues",
                column: "parentIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_issueId",
                table: "comments",
                column: "issueId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_issues_parentIssueId",
                table: "issues",
                column: "parentIssueId",
                principalTable: "issues",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_users_lastUpdateById",
                table: "issues",
                column: "lastUpdateById",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_issues_parentIssueId",
                table: "issues");

            migrationBuilder.DropForeignKey(
                name: "FK_issues_users_lastUpdateById",
                table: "issues");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropIndex(
                name: "IX_issues_lastUpdateById",
                table: "issues");

            migrationBuilder.DropIndex(
                name: "IX_issues_parentIssueId",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "index",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "lastUpdateById",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "parentIssueId",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "position",
                table: "issues");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "issues",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
