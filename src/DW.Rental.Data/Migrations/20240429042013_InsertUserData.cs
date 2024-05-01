using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Rendal.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "User",
               schema: "dbo",
               columns: new[] { "Id", "Username", "Password", "Role", "Status", "CreatedAt" },
               columnTypes: new[] { "uuid", "nvarchar(50)", "nvarchar(50)", "nvarchar(50)", "int", "datetime2" },
               values: new object[] { Guid.NewGuid(), "admin", "admin", "Admin", 1, DateTime.UtcNow });


            var deliverymanUserId = Guid.NewGuid();
            migrationBuilder.InsertData(
               table: "User",
               schema: "dbo",
               columns: new[] { "Id", "Username", "Password", "Role", "Status", "CreatedAt" },
               columnTypes: new[] { "uuid", "nvarchar(50)", "nvarchar(50)", "nvarchar(50)", "int", "datetime2" },
               values: new object[] { deliverymanUserId, "douglas", "123456", "Deliveryman", 1, DateTime.UtcNow });

            migrationBuilder.InsertData(
               table: "Deliveryman",
               schema: "dbo",
               columns: new[] { "Id", "Name", "Cnpj", "Birthday", "CNH", "CnhType", "CnhPhoto", "Status", "CreatedAt", "UserId" },
               columnTypes: new[] { "uuid", "nvarchar(50)", "nvarchar(50)", "datetime2", "int", "int", "nvarchar(50)", "int", "datetime2", "uuid" },
               values: new object[] { Guid.NewGuid(), "douglas", "1526548976", DateTime.UtcNow.AddYears(-27), 84868468, 0, "", 1, DateTime.UtcNow, deliverymanUserId });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
