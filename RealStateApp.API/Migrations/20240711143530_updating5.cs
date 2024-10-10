using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Migrations
{
    /// <inheritdoc />
    public partial class updating5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserEmail",
                keyValue: null,
                column: "UserEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UserID",
                table: "Users",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldDefaultValue: new byte[] { 210, 152, 157, 2, 135, 158, 202, 74, 148, 82, 140, 111, 249, 144, 174, 114 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UserID",
                table: "Users",
                type: "binary(16)",
                nullable: false,
                defaultValue: new byte[] { 210, 152, 157, 2, 135, 158, 202, 74, 148, 82, 140, 111, 249, 144, 174, 114 },
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");
        }
    }
}
