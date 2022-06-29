using Microsoft.EntityFrameworkCore.Migrations;

namespace BookstoreProject.Data.Migrations
{
    public partial class temelTablolar3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Kategoriler");

            migrationBuilder.AddColumn<int>(
                name: "DilId",
                table: "Kitaplar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ad_EN",
                table: "Kategoriler",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ad_TR",
                table: "Kategoriler",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Diller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad_TR = table.Column<string>(nullable: true),
                    Ad_EN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diller", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_DilId",
                table: "Kitaplar",
                column: "DilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Diller_DilId",
                table: "Kitaplar",
                column: "DilId",
                principalTable: "Diller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Diller_DilId",
                table: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Diller");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_DilId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "DilId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "Ad_EN",
                table: "Kategoriler");

            migrationBuilder.DropColumn(
                name: "Ad_TR",
                table: "Kategoriler");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Kategoriler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
