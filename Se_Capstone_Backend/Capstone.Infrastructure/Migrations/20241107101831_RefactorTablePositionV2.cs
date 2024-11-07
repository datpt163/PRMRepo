using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTablePositionV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "positions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "createdBy",
                table: "positions",
                type: "uuid",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "positions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "positions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "updatedBy",
                table: "positions",
                type: "uuid",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "positions");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "positions");
        }
    }
}
