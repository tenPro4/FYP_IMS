using Microsoft.EntityFrameworkCore.Migrations;

namespace IWMS.Migrations
{
    public partial class addDescriptionToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");
        }
    }
}
