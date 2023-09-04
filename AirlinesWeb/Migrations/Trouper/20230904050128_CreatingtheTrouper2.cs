using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlinesWeb.Migrations.Trouper
{
    /// <inheritdoc />
    public partial class CreatingtheTrouper2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "passenger_name",
                schema: "bookings",
                table: "troupers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "bookings",
                table: "troupers",
                type: "varchar(35)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "passenger_name",
                schema: "bookings",
                table: "troupers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "bookings",
                table: "troupers",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)");
        }
    }
}
