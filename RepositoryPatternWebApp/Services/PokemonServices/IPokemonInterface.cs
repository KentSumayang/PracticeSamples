using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWebApp.Models;

namespace RepositoryPatternWebApp.Services.PokemonServices
{
    public interface IPokemonInterface
    {
        IEnumerable<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        IActionResult PutPokemon (int id, Pokemon pokemon);
        Pokemon PostPokemon(Pokemon pokemon);
        IActionResult DeletePokemon (int id);

    }
}
