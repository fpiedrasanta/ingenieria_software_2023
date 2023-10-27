using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_programacion_iii.Migrations
{
    public partial class alter_resource_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResourceTypeId",
                table: "Resources",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceTypes_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceTypes_ResourceTypeId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "Resources");
        }
    }
}
