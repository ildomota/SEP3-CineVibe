using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineVibeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSnackOrderSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stock",
                schema: "cinevibe",
                table: "snacks");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "cinevibe",
                table: "snacks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "imagepath",
                schema: "cinevibe",
                table: "snacks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                schema: "cinevibe",
                table: "snackorders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "booking_id",
                schema: "cinevibe",
                table: "snackorders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "discountedprice",
                schema: "cinevibe",
                table: "snackorders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "cinevibe",
                table: "customer",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_snackorders_CustomerId1",
                schema: "cinevibe",
                table: "snackorders",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_snackorders_SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders",
                column: "SnacksSnackId");

            migrationBuilder.AddForeignKey(
                name: "FK_snackorders_customer_CustomerId1",
                schema: "cinevibe",
                table: "snackorders",
                column: "CustomerId1",
                principalSchema: "cinevibe",
                principalTable: "customer",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_snackorders_snacks_SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders",
                column: "SnacksSnackId",
                principalSchema: "cinevibe",
                principalTable: "snacks",
                principalColumn: "snackid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_snackorders_customer_CustomerId1",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropForeignKey(
                name: "FK_snackorders_snacks_SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropIndex(
                name: "IX_snackorders_CustomerId1",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropIndex(
                name: "IX_snackorders_SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropColumn(
                name: "imagepath",
                schema: "cinevibe",
                table: "snacks");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropColumn(
                name: "SnacksSnackId",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropColumn(
                name: "booking_id",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.DropColumn(
                name: "discountedprice",
                schema: "cinevibe",
                table: "snackorders");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "cinevibe",
                table: "snacks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "stock",
                schema: "cinevibe",
                table: "snacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "cinevibe",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "cinevibe",
                table: "admin",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
