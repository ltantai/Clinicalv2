using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinical.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class notecolofpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Patients");
        }
    }
}
