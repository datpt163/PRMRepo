using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommonFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "logEntries");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "leave_logs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
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
                name: "updateAt",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "updatedAt",
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
                name: "updateAt",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "applicants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "projects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "logEntries",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "leave_logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "leave_logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "jobs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "jobs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "jobs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "jobs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                table: "jobs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "attendances",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "articles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "articles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "applicants",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "applicants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "applicants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "applicants",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                table: "applicants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
