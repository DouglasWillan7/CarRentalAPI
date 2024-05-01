using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMotorcycleCollunmName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Motocycle_Placa",
                schema: "dbo",
                table: "Motocycle");

            migrationBuilder.RenameColumn(
                name: "Placa",
                schema: "dbo",
                table: "Motocycle",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                schema: "dbo",
                table: "Motocycle",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "Ano",
                schema: "dbo",
                table: "Motocycle",
                newName: "Year");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "dbo",
                table: "Motocycle",
                newName: "Ano");

            migrationBuilder.RenameColumn(
                name: "Model",
                schema: "dbo",
                table: "Motocycle",
                newName: "Placa");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                schema: "dbo",
                table: "Motocycle",
                newName: "Modelo");

            migrationBuilder.CreateIndex(
                name: "IX_Motocycle_Placa",
                schema: "dbo",
                table: "Motocycle",
                column: "Placa",
                unique: true);
        }
    }
}
