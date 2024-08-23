using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTableJobV0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicants_jobs_jobId",
                table: "applicants");

            migrationBuilder.DropIndex(
                name: "IX_applicants_jobId",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "applicants");
        }
    }
}
