using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rental",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliverymanId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Plan = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rental_Deliveryman_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalSchema: "dbo",
                        principalTable: "Deliveryman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_Motocycle_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalSchema: "dbo",
                        principalTable: "Motocycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_DeliverymanId",
                schema: "dbo",
                table: "Rental",
                column: "DeliverymanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Id",
                schema: "dbo",
                table: "Rental",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_MotorcycleId",
                schema: "dbo",
                table: "Rental",
                column: "MotorcycleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental",
                schema: "dbo");
        }
    }
}
