using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "labels");

            migrationBuilder.AddColumn<Guid>(
                name: "positionId",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "statuses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "statuses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "statuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "projectId",
                table: "statuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "labels",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "projectId",
                table: "labels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_positionId",
                table: "users",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_statuses_projectId",
                table: "statuses",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_labels_projectId",
                table: "labels",
                column: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_labels_projects_projectId",
                table: "labels",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_statuses_projects_projectId",
                table: "statuses",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_positions_positionId",
                table: "users",
                column: "positionId",
                principalTable: "positions",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_labels_projects_projectId",
                table: "labels");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_projects_projectId",
                table: "statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_users_positions_positionId",
                table: "users");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropIndex(
                name: "IX_users_positionId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_statuses_projectId",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_labels_projectId",
                table: "labels");

            migrationBuilder.DropColumn(
                name: "positionId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "color",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "position",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "projectId",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "projectId",
                table: "labels");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "statuses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "statuses",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "labels",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "labels",
                type: "boolean",
                nullable: true);
        }
    }
}
