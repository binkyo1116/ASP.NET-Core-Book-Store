using Microsoft.EntityFrameworkCore.Migrations;

namespace BookstoreProject.Data.Migrations
{
    public partial class temelTablolar6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Languages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Categories");
        }
    }
}
