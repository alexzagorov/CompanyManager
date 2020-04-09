using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMe.Data.Migrations
{
    public partial class AddTextPropertyToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Messages");
        }
    }
}
