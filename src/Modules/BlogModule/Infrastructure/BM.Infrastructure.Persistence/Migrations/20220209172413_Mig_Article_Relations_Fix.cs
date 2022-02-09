using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM.Infrastructure.Persistence.Migrations
{
    public partial class Mig_Article_Relations_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_CategoyId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CategoyId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CategoyId",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_CategoryId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles");

            migrationBuilder.AddColumn<long>(
                name: "CategoyId",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoyId",
                table: "Articles",
                column: "CategoyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_CategoyId",
                table: "Articles",
                column: "CategoyId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
