using Microsoft.EntityFrameworkCore.Migrations;

namespace RuneAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Runewords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runewords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    RunewordId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifier_Runewords_RunewordId",
                        column: x => x.RunewordId,
                        principalTable: "Runewords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RuneRuneword",
                columns: table => new
                {
                    RUNEWORDSId = table.Column<int>(type: "INTEGER", nullable: false),
                    RunesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuneRuneword", x => new { x.RUNEWORDSId, x.RunesId });
                    table.ForeignKey(
                        name: "FK_RuneRuneword_Runes_RunesId",
                        column: x => x.RunesId,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuneRuneword_Runewords_RUNEWORDSId",
                        column: x => x.RUNEWORDSId,
                        principalTable: "Runewords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modifier_RunewordId",
                table: "Modifier",
                column: "RunewordId");

            migrationBuilder.CreateIndex(
                name: "IX_RuneRuneword_RunesId",
                table: "RuneRuneword",
                column: "RunesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modifier");

            migrationBuilder.DropTable(
                name: "RuneRuneword");

            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "Runewords");
        }
    }
}
