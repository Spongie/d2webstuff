using Microsoft.EntityFrameworkCore.Migrations;

namespace RuneAPI.Migrations
{
    public partial class AddRquiredLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RequiredLevel",
                table: "Runewords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredLevel",
                table: "Runewords");
        }
    }
}
