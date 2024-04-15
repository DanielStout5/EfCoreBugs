using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreOpenJson.Migrations
{
    /// <inheritdoc />
    public partial class AddBookPublishAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HideAt",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishAt",
                table: "Books",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HideAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublishAt",
                table: "Books");
        }
    }
}
