using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlinesApi.Database.Migrations.Airlines
{
    /// <inheritdoc />
    public partial class AddingUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "bookings",
                table: "travellers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "bookings",
                table: "travellers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                schema: "bookings",
                table: "travellers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                schema: "bookings",
                table: "travellers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                schema: "bookings",
                table: "travellers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                schema: "bookings",
                table: "travellers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "bookings",
                table: "travellers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "bookings",
                table: "travellers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "bookings",
                table: "travellers");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "bookings",
                table: "travellers");
        }
    }
}
