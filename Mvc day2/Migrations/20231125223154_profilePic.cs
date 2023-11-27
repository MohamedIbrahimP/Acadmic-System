using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_day2.Migrations
{
    /// <inheritdoc />
    public partial class profilePic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Instructors");

            migrationBuilder.AddColumn<byte[]>(
                name: "profileImage",
                table: "Instructors",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profileImage",
                table: "Instructors");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
