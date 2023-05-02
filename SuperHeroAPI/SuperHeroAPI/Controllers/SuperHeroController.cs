using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Services.SuperHeroService;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public  SuperHeroController(ISuperHeroService superHeroService)
        {
             _superHeroService = superHeroService;
        }
        [HttpGet("GetAllHeroes")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var result = _superHeroService.GetAllHeroes();
            return Ok(result);
        }
        [HttpGet("GetSingleHero")]
        public async Task<ActionResult<List<SuperHero>>> GetSingleHero(int id)
        {
            var result = _superHeroService.GetSingleHero(id);
            return Ok(result);
        }
        [HttpPost("AddHero")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            var result = _superHeroService.AddHero(hero);
            return Ok(result);
        }
        [HttpPut(" UpdateHero")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero request)
        {
            var result = _superHeroService.UpdateHero(id, request);
            if (result == null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
        [HttpDelete("DeleteHero")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var result = _superHeroService.DeleteHero(id);
            if (result == null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
