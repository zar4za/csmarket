using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CsMarket.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetClass",
                columns: table => new
                {
                    ClassId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MarketHashName = table.Column<string>(type: "text", nullable: false),
                    IconUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetClass", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    SteamId32 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SignupUnixMilli = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AvatarHash = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    TradeLink = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.SteamId32);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerSteamId32 = table.Column<int>(type: "integer", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    WasTraded = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_AssetClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "AssetClass",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Users_OwnerSteamId32",
                        column: x => x.OwnerSteamId32,
                        principalTable: "Users",
                        principalColumn: "SteamId32",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ClassId",
                table: "Assets",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OwnerSteamId32",
                table: "Assets",
                column: "OwnerSteamId32");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_AssetId",
                table: "Listings",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetClass");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
