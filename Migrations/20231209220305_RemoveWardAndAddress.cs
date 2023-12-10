using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacramentMeetingPlanner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWardAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "Hymn",
                table: "Meetings",
                newName: "SacramentHymn");

            migrationBuilder.AlterColumn<string>(
                name: "Ward",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SacramentHymn",
                table: "Meetings",
                newName: "Hymn");

            migrationBuilder.AlterColumn<string>(
                name: "Ward",
                table: "Meetings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Meetings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
