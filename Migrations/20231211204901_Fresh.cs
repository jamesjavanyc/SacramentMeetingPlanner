using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacramentMeetingPlanner.Migrations
{
    /// <inheritdoc />
    public partial class Fresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Leader = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpeningSong = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OpeningPrayer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SacramentHymn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MusicNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClosingSong = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClosingPrayer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Speaker = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talk_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talk_MeetingId",
                table: "Talk",
                column: "MeetingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talk");

            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
