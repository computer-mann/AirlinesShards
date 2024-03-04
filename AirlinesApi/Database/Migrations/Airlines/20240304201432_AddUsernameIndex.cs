using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlinesApi.Database.Migrations.Airlines
{
    /// <inheritdoc />
    public partial class AddUsernameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "fk_passenger_id_travellers",
            //    schema: "bookings",
            //    table: "tickets");

            migrationBuilder.CreateIndex(
                name: "idx_username",
                schema: "bookings",
                table: "travellers",
                column: "NormalizedUserName",
                unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_tickets_travellers_passenger_id",
            //    schema: "bookings",
            //    table: "tickets",
            //    column: "passenger_id",
            //    principalSchema: "bookings",
            //    principalTable: "travellers",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_tickets_travellers_passenger_id",
            //    schema: "bookings",
            //    table: "tickets");

            //migrationBuilder.DropIndex(
            //    name: "idx_username",
            //    schema: "bookings",
            //    table: "travellers");

            //migrationBuilder.AddForeignKey(
            //    name: "fk_passenger_id_travellers",
            //    schema: "bookings",
            //    table: "tickets",
            //    column: "passenger_id",
            //    principalSchema: "bookings",
            //    principalTable: "travellers",
            //    principalColumn: "Id");
        }
    }
}
