using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWebApp.Data;
using RepositoryPatternWebApp.Models;
using RepositoryPatternWebApp.Services.PokemonServices;

namespace RepositoryPatternWebApp.Services.PokemonServices
{
    public class PokemonService : IPokemonInterface
    {
        private readonly PokemonContext _context;

        public PokemonService(PokemonContext context)
        {
            _context = context;
        }
        public IActionResult? DeletePokemon (int id)
        {
            var pokemon = _context.Pokemons.Find(id);

            _context.Pokemons.Remove(pokemon);
            _context.SaveChangesAsync();

            return null;

        }

        public Pokemon GetPokemon(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pokemon> GetPokemons()
        {
            throw new NotImplementedException();
        }

        public Pokemon PostPokemon(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public IActionResult PutPokemon(int id, Pokemon pokemon)
        {
            throw new NotImplementedException();
        }
    }
}
