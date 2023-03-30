using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsMarket.Migrations
{
    public partial class Rarity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rarity",
                table: "AssetClass",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "AssetClass");
        }
    }
}
