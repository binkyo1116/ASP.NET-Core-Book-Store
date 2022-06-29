using Microsoft.EntityFrameworkCore.Migrations;

namespace BookstoreProject.Data.Migrations
{
    public partial class temelTablolar5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPhoto",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPhoto",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SecondPhoto",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ThirdPhoto",
                table: "Books");
        }
    }
}
