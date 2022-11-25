using Microsoft.EntityFrameworkCore.Migrations;

namespace inzynierkav2.Migrations
{
    public partial class iswatched_itemsinstorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsWatched",
                table: "StorageItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWatched",
                table: "StorageItems");
        }
    }
}
