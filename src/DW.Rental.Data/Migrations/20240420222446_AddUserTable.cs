using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "Deliveryman",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveryman_Cnpj",
                schema: "dbo",
                table: "Deliveryman",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveryman_UserId",
                schema: "dbo",
                table: "Deliveryman",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                schema: "dbo",
                table: "User",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveryman_User_UserId",
                schema: "dbo",
                table: "Deliveryman",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveryman_User_UserId",
                schema: "dbo",
                table: "Deliveryman");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Deliveryman_Cnpj",
                schema: "dbo",
                table: "Deliveryman");

            migrationBuilder.DropIndex(
                name: "IX_Deliveryman_UserId",
                schema: "dbo",
                table: "Deliveryman");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Deliveryman");
        }
    }
}
