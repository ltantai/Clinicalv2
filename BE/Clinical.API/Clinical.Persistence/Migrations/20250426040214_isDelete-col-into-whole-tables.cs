using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinical.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class isDeletecolintowholetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Doctors");
        }
    }
}
