using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Training.API.Users.Database.Migrations
{
    public partial class AddedInfoWhoAddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<LocalDateTime>(
                name: "CreatedAt",
                schema: "users",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new NodaTime.LocalDateTime(1, 1, 1, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "ModifiedAt",
                schema: "users",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy_FullName",
                schema: "users",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy_Id",
                schema: "users",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "CreatedAt",
                schema: "users",
                table: "Permissions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new NodaTime.LocalDateTime(1, 1, 1, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "CreatedAt",
                schema: "users",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new NodaTime.LocalDateTime(1, 1, 1, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "ModifiedAt",
                schema: "users",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy_FullName",
                schema: "users",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy_Id",
                schema: "users",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<LocalDateTime>(
                name: "CreatedAt",
                schema: "users",
                table: "GroupMembers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new NodaTime.LocalDateTime(1, 1, 1, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "GroupMembers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy_Id",
                schema: "users",
                table: "GroupMembers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy_FullName",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy_Id",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "users",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedBy_Id",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifiedBy_FullName",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifiedBy_Id",
                schema: "users",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "users",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy_FullName",
                schema: "users",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy_Id",
                schema: "users",
                table: "GroupMembers");
        }
    }
}
