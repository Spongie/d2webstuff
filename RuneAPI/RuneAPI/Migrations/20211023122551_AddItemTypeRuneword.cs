using Microsoft.EntityFrameworkCore.Migrations;

namespace RuneAPI.Migrations
{
    public partial class AddItemTypeRuneword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetTypes",
                table: "Runewords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetTypes",
                table: "Runewords");
        }
    }
}
