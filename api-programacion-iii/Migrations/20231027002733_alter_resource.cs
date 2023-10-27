using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_programacion_iii.Migrations
{
    public partial class alter_resource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Resources",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Resources");
        }
    }
}
