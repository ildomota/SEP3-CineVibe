using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineVibeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToLoyaltyPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "posterimageurl",
                schema: "cinevibe",
                table: "movies",
                newName: "posterurl");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                schema: "cinevibe",
                table: "moviereviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                schema: "cinevibe",
                table: "moviereviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                schema: "cinevibe",
                table: "loyaltypoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "cinevibe",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "seatnumber",
                schema: "cinevibe",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bookingtime",
                schema: "cinevibe",
                table: "bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdat",
                schema: "cinevibe",
                table: "moviereviews");

            migrationBuilder.DropColumn(
                name: "updatedat",
                schema: "cinevibe",
                table: "moviereviews");

            migrationBuilder.DropColumn(
                name: "updatedat",
                schema: "cinevibe",
                table: "loyaltypoints");

            migrationBuilder.DropColumn(
                name: "bookingtime",
                schema: "cinevibe",
                table: "bookings");

            migrationBuilder.RenameColumn(
                name: "posterurl",
                schema: "cinevibe",
                table: "movies",
                newName: "posterimageurl");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "cinevibe",
                table: "bookings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "seatnumber",
                schema: "cinevibe",
                table: "bookings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
