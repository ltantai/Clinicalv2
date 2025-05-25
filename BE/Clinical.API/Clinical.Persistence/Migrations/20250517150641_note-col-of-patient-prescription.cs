using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinical.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class notecolofpatientprescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "PatientPrescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "PatientPrescriptions");
        }
    }
}
