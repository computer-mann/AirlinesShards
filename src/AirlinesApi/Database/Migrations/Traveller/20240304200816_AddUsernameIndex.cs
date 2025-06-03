using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlinesApi.Database.Migrations.Traveller
{
    /// <inheritdoc />
    public partial class AddUsernameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Travellers",
            //    schema: "bookings",
            //    table: "Travellers");

            //migrationBuilder.RenameTable(
            //    name: "Travellers",
            //    schema: "bookings",
            //    newName: "travellers",
            //    newSchema: "bookings");

            migrationBuilder.RenameIndex(
                name: "UserNameIndex",
                schema: "bookings",
                table: "travellers",
                newName: "idx_username");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_travellers",
            //    schema: "bookings",
            //    table: "travellers",
            //    column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_travellers",
            //    schema: "bookings",
            //    table: "travellers");

            //migrationBuilder.RenameTable(
            //    name: "travellers",
            //    schema: "bookings",
            //    newName: "Travellers",
            //    newSchema: "bookings");

            //migrationBuilder.RenameIndex(
            //    name: "idx_username",
            //    schema: "bookings",
            //    table: "Travellers",
            //    newName: "UserNameIndex");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Travellers",
            //    schema: "bookings",
            //    table: "Travellers",
            //    column: "Id");
        }
    }
}
