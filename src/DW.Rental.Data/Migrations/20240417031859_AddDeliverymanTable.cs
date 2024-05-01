using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations;

/// <inheritdoc />
public partial class AddDeliverymanTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Deliveryman",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nome = table.Column<int>(type: "integer", nullable: false),
                Cnpj = table.Column<string>(type: "text", nullable: false),
                DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                CNH = table.Column<int>(type: "integer", nullable: false),
                TipoCnh = table.Column<int>(type: "integer", nullable: false),
                FotoCnh = table.Column<string>(type: "text", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Deliveryman", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Deliveryman_CNH",
            schema: "dbo",
            table: "Deliveryman",
            column: "CNH",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Deliveryman_Id",
            schema: "dbo",
            table: "Deliveryman",
            column: "Id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Deliveryman",
            schema: "dbo");
    }
}
