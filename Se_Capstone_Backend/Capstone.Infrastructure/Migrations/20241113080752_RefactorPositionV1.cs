using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorPositionV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_positions_positionId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_positionId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "positionId",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "positions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "positions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "positionId",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "positions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "positions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_positionId",
                table: "users",
                column: "positionId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_positions_positionId",
                table: "users",
                column: "positionId",
                principalTable: "positions",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
