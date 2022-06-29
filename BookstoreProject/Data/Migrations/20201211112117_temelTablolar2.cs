using Microsoft.EntityFrameworkCore.Migrations;

namespace BookstoreProject.Data.Migrations
{
    public partial class temelTablolar2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitap_Kategori_KategoriId",
                table: "Kitap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kitap",
                table: "Kitap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategori",
                table: "Kategori");

            migrationBuilder.RenameTable(
                name: "Kitap",
                newName: "Kitaplar");

            migrationBuilder.RenameTable(
                name: "Kategori",
                newName: "Kategoriler");

            migrationBuilder.RenameIndex(
                name: "IX_Kitap_KategoriId",
                table: "Kitaplar",
                newName: "IX_Kitaplar_KategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kitaplar",
                table: "Kitaplar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategoriler",
                table: "Kategoriler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriId",
                table: "Kitaplar",
                column: "KategoriId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriId",
                table: "Kitaplar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kitaplar",
                table: "Kitaplar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategoriler",
                table: "Kategoriler");

            migrationBuilder.RenameTable(
                name: "Kitaplar",
                newName: "Kitap");

            migrationBuilder.RenameTable(
                name: "Kategoriler",
                newName: "Kategori");

            migrationBuilder.RenameIndex(
                name: "IX_Kitaplar_KategoriId",
                table: "Kitap",
                newName: "IX_Kitap_KategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kitap",
                table: "Kitap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategori",
                table: "Kategori",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitap_Kategori_KategoriId",
                table: "Kitap",
                column: "KategoriId",
                principalTable: "Kategori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
