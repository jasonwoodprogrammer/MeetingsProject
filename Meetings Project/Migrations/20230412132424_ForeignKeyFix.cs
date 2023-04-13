using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meetings_Project.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingPerson");

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Persons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MeetingId",
                table: "Persons",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Meetings_MeetingId",
                table: "Persons",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Meetings_MeetingId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_MeetingId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "MeetingPerson",
                columns: table => new
                {
                    MeetingsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingPerson", x => new { x.MeetingsId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_MeetingPerson_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPerson_PersonsId",
                table: "MeetingPerson",
                column: "PersonsId");
        }
    }
}
