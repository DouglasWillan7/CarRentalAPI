using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeliverymanCollunName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoCnh",
                schema: "dbo",
                table: "Deliveryman",
                newName: "CnhType");

            migrationBuilder.RenameColumn(
                name: "Nome",
                schema: "dbo",
                table: "Deliveryman",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FotoCnh",
                schema: "dbo",
                table: "Deliveryman",
                newName: "CnhPhoto");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                schema: "dbo",
                table: "Deliveryman",
                newName: "Birthday");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "Deliveryman",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "CnhType",
                schema: "dbo",
                table: "Deliveryman",
                newName: "TipoCnh");

            migrationBuilder.RenameColumn(
                name: "CnhPhoto",
                schema: "dbo",
                table: "Deliveryman",
                newName: "FotoCnh");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                schema: "dbo",
                table: "Deliveryman",
                newName: "DataNascimento");
        }
    }
}
