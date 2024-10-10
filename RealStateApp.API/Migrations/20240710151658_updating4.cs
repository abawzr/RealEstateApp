using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Migrations
{
    /// <inheritdoc />
    public partial class updating4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "UserID",
                table: "Users",
                type: "binary(16)",
                nullable: false,
                defaultValue: new byte[] { 210, 152, 157, 2, 135, 158, 202, 74, 148, 82, 140, 111, 249, 144, 174, 114 },
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "UserID",
                table: "Users",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldDefaultValue: new byte[] { 210, 152, 157, 2, 135, 158, 202, 74, 148, 82, 140, 111, 249, 144, 174, 114 });
        }
    }
}
