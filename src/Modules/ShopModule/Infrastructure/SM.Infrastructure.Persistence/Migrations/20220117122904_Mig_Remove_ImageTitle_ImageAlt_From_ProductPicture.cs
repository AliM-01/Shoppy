using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infrastructure.Persistence.Migrations
{
    public partial class Mig_Remove_ImageTitle_ImageAlt_From_ProductPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAlt",
                table: "ProductPictures");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "ProductPictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageAlt",
                table: "ProductPictures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "ProductPictures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
