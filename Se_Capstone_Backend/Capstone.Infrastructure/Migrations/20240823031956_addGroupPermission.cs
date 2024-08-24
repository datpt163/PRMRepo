using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addGroupPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "groupPermissionId",
                table: "permissions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permissions_groupPermissionId",
                table: "permissions",
                column: "groupPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_GroupPermissions_groupPermissionId",
                table: "permissions",
                column: "groupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_GroupPermissions_groupPermissionId",
                table: "permissions");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropIndex(
                name: "IX_permissions_groupPermissionId",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "groupPermissionId",
                table: "permissions");
        }
    }
}
