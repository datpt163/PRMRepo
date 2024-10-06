using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTableApplicantV0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicants_jobs_jobId",
                table: "applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_applicants_users_userId",
                table: "applicants");

            migrationBuilder.DropIndex(
                name: "IX_applicants_jobId",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "applicants");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "applicants",
                newName: "mainJobId");

            migrationBuilder.RenameIndex(
                name: "IX_applicants_userId",
                table: "applicants",
                newName: "IX_applicants_mainJobId");

            migrationBuilder.AlterColumn<string>(
                name: "cvLink",
                table: "applicants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_applicants_jobs_mainJobId",
                table: "applicants",
                column: "mainJobId",
                principalTable: "jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicants_jobs_mainJobId",
                table: "applicants");

            migrationBuilder.RenameColumn(
                name: "mainJobId",
                table: "applicants",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_applicants_mainJobId",
                table: "applicants",
                newName: "IX_applicants_userId");

            migrationBuilder.AlterColumn<string>(
                name: "cvLink",
                table: "applicants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "jobId",
                table: "applicants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_applicants_jobId",
                table: "applicants",
                column: "jobId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicants_jobs_jobId",
                table: "applicants",
                column: "jobId",
                principalTable: "jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_applicants_users_userId",
                table: "applicants",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
