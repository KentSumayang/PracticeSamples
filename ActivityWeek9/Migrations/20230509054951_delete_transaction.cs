using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivityWeek9.Migrations
{
    /// <inheritdoc />
    public partial class delete_transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Transactions__Accounts_AccountId",
                table: "_Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK__Users__Accounts_AccountId",
                table: "_Users");

            migrationBuilder.DropForeignKey(
                name: "FK__Users__Informations_InformationId",
                table: "_Users");

            migrationBuilder.DropTable(
                name: "_Accounts");

            migrationBuilder.DropTable(
                name: "_Informations");

            migrationBuilder.DropIndex(
                name: "IX__Users_AccountId",
                table: "_Users");

            migrationBuilder.DropIndex(
                name: "IX__Users_InformationId",
                table: "_Users");

            migrationBuilder.DropIndex(
                name: "IX__Transactions_AccountId",
                table: "_Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "InformationId",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "_Transactions");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "_Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "_Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "_Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "_Users");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "_Users");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "_Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InformationId",
                table: "_Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "_Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "_Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Informations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Informations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX__Users_AccountId",
                table: "_Users",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX__Users_InformationId",
                table: "_Users",
                column: "InformationId");

            migrationBuilder.CreateIndex(
                name: "IX__Transactions_AccountId",
                table: "_Transactions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK__Transactions__Accounts_AccountId",
                table: "_Transactions",
                column: "AccountId",
                principalTable: "_Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Accounts_AccountId",
                table: "_Users",
                column: "AccountId",
                principalTable: "_Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__Informations_InformationId",
                table: "_Users",
                column: "InformationId",
                principalTable: "_Informations",
                principalColumn: "Id");
        }
    }
}
