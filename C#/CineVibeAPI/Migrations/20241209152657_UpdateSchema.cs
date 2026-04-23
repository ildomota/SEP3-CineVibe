using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineVibeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cinevibe");

            migrationBuilder.RenameTable(
                name: "snacks",
                newName: "snacks",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "snackorders",
                newName: "snackorders",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "promotions",
                newName: "promotions",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "notifications",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "movies",
                newName: "movies",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "moviereviews",
                newName: "moviereviews",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "loyaltypoints",
                newName: "loyaltypoints",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "customer",
                newName: "customer",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "bookings",
                newSchema: "cinevibe");

            migrationBuilder.RenameTable(
                name: "admin",
                newName: "admin",
                newSchema: "cinevibe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "snacks",
                schema: "cinevibe",
                newName: "snacks");

            migrationBuilder.RenameTable(
                name: "snackorders",
                schema: "cinevibe",
                newName: "snackorders");

            migrationBuilder.RenameTable(
                name: "promotions",
                schema: "cinevibe",
                newName: "promotions");

            migrationBuilder.RenameTable(
                name: "notifications",
                schema: "cinevibe",
                newName: "notifications");

            migrationBuilder.RenameTable(
                name: "movies",
                schema: "cinevibe",
                newName: "movies");

            migrationBuilder.RenameTable(
                name: "moviereviews",
                schema: "cinevibe",
                newName: "moviereviews");

            migrationBuilder.RenameTable(
                name: "loyaltypoints",
                schema: "cinevibe",
                newName: "loyaltypoints");

            migrationBuilder.RenameTable(
                name: "customer",
                schema: "cinevibe",
                newName: "customer");

            migrationBuilder.RenameTable(
                name: "bookings",
                schema: "cinevibe",
                newName: "bookings");

            migrationBuilder.RenameTable(
                name: "admin",
                schema: "cinevibe",
                newName: "admin");
        }
    }
}
