using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "Motocycle",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Ano = table.Column<int>(type: "integer", nullable: false),
                Placa = table.Column<string>(type: "text", nullable: false),
                Modelo = table.Column<string>(type: "text", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Motocycle", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Motocycle_Id",
            schema: "dbo",
            table: "Motocycle",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Motocycle_Placa",
            schema: "dbo",
            table: "Motocycle",
            column: "Placa",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Motocycle",
            schema: "dbo");
    }
}
