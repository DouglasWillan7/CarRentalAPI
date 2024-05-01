using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations;

/// <inheritdoc />
public partial class UpdateTypeCollunmName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Nome",
            schema: "dbo",
            table: "Deliveryman",
            type: "text",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Nome",
            schema: "dbo",
            table: "Deliveryman",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
    }
}
