using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    public partial class UserPasswordAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ErsmsUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ErsmsUsers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ErsmsUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ErsmsUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "ErsmsUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "ErsmsUsers",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ErsmsUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ErsmsUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ErsmsUsers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "ErsmsUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ErsmsUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ErsmsUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
