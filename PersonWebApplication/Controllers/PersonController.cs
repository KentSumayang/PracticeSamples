using Microsoft.AspNetCore.Mvc;
using PersonWebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static List<Person> persons = new List<Person>
        {


        };
       
        [HttpPost("AddPerson")]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            
            int maximum = 2;
            try
            {
                if (persons.Count == maximum)
                {
                    return BadRequest("Maximum Reached.");
                    
                }
                else
                {
                    persons.Add(person);
                    return persons;

                }
            }
            catch (Exception)
            {

                throw;
            }


            
        }
        [HttpGet("ShowPersons")]
        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            persons.Reverse();
            return persons;
        }



    }
}
