using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_programacion_iii.Migrations
{
    public partial class add_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "Resources");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Resources",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "longtext", nullable: false),
                    Url = table.Column<string>(type: "longtext", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ImageId",
                table: "Resources",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Images_ImageId",
                table: "Resources",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Images_ImageId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ImageId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Resources");

            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "Resources",
                type: "longtext",
                nullable: false);
        }
    }
}
