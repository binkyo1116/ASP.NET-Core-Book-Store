using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookstoreProject.Data.Migrations
{
    public partial class sepetTablosuUserDuzenleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Baskets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserDetailsId",
                table: "Baskets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserDetailsId",
                table: "Baskets",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_UserDetailsId",
                table: "Baskets",
                column: "UserDetailsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_UserDetailsId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserDetailsId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "Baskets");

            migrationBuilder.AddColumn<string>(
                name: "Durum",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Baskets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
