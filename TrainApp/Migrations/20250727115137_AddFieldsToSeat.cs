using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAva",
                table: "Seats",
                newName: "IsAvailable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Seats",
                newName: "IsAva");
        }
    }
}
