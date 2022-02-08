using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Debit_Cards_Project.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Holder_HolderId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "Holder");

            migrationBuilder.CreateTable(
                name: "Holders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Holders_HolderId",
                table: "Cards",
                column: "HolderId",
                principalTable: "Holders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Holders_HolderId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "Holders");

            migrationBuilder.CreateTable(
                name: "Holder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holder", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Holder_HolderId",
                table: "Cards",
                column: "HolderId",
                principalTable: "Holder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
