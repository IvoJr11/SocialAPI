using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApi_NET7.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UUID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AuthorUUID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorUUID",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");
        }
    }
}
