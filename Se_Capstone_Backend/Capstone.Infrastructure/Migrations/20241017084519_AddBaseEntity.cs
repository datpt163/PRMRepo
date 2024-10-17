using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "projects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "projects",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "projects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "projects",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "logEntries",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "logEntries",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "logEntries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "logEntries",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "logEntries",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "leave_logs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "leave_logs",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "leave_logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "leave_logs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "leave_logs",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "jobs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "jobs",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "jobs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "jobs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "jobs",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "issues",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "issues",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "issues",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "issues",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "issues",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "comments",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "comments",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "attendances",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "attendances",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "attendances",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "attendances",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "attendances",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "articles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "articles",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "articles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "articles",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "applicants",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "applicants",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "applicants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "applicants",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "applicants",
                type: "uuid",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "applicants");
        }
    }
}
