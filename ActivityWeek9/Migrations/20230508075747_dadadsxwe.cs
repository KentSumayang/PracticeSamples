using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityWeek9.Migrations
{
    /// <inheritdoc />
    public partial class dadadsxwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Informations__Accounts_AccountId",
                table: "_Informations");

            migrationBuilder.DropIndex(
                name: "IX__Informations_AccountId",
                table: "_Informations");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "_Informations");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "_Informations",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "_Accounts",
                newName: "IsActive");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "_Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__Users_AccountId",
                table: "_Users",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Accounts_AccountId",
                table: "_Users",
                column: "AccountId",
                principalTable: "_Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Users__Accounts_AccountId",
                table: "_Users");

            migrationBuilder.DropIndex(
                name: "IX__Users_AccountId",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "_Users");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "_Informations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "_Accounts",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "_Informations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__Informations_AccountId",
                table: "_Informations",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK__Informations__Accounts_AccountId",
                table: "_Informations",
                column: "AccountId",
                principalTable: "_Accounts",
                principalColumn: "Id");
        }
    }
}
