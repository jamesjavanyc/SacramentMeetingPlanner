using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacramentMeetingPlanner.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetingClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClosingPrayer",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClosingSong",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hymn",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Leader",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MusicNumber",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OpeningPrayer",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OpeningSong",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingPrayer",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "ClosingSong",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Hymn",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Leader",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MusicNumber",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "OpeningPrayer",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "OpeningSong",
                table: "Meetings");
        }
    }
}
