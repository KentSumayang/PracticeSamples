using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryPatternWebApp.Migrations
{
    /// <inheritdoc />
    public partial class changeidmodelpokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "Pokemons",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pokemons",
                newName: "PokemonId");
        }
    }
}
