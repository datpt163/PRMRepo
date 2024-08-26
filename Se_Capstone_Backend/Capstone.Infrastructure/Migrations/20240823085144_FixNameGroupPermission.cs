using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNameGroupPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_GroupPermissions_groupPermissionId",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                newName: "groupPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "groupPermissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_groupPermissions",
                table: "groupPermissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_groupPermissions_groupPermissionId",
                table: "permissions",
                column: "groupPermissionId",
                principalTable: "groupPermissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_groupPermissions_groupPermissionId",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_groupPermissions",
                table: "groupPermissions");

            migrationBuilder.RenameTable(
                name: "groupPermissions",
                newName: "GroupPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "GroupPermissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_GroupPermissions_groupPermissionId",
                table: "permissions",
                column: "groupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
