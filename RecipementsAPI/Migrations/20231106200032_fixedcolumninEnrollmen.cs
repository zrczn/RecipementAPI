using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipementAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedcolumninEnrollmen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipementId",
                table: "Enrollments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipementId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
