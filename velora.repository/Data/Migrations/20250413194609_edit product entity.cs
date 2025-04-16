using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace velora.repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class editproductentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkinType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkinType",
                table: "Products");
        }
    }
}
