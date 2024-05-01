using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLicensePlateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Motocycle_LicensePlate",
                schema: "dbo",
                table: "Motocycle",
                column: "LicensePlate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "IX_Motocycle_LicensePlate",
               schema: "dbo",
               table: "Motocycle");
        }
    }
}
