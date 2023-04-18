using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PersonWebApplication.Models;

namespace PersonWebApplication.Controllers
{
    public class SampleController : Controller
    {
        [HttpPost("AddInformation")]
        public async Task<ActionResult<Sample>> AddInformation(int id, string name)
        {
            Sample sample = new Sample(id, name);
            string query = "INSERT INTO sampletbl (id,name) values('" + id + "','" + name + "')";
            Connection conn = new Connection();

            MySqlCommand command = new MySqlCommand(query, conn.GetConnection());
            MySqlDataReader reader;

            conn.OpenConnection();

            reader = command.ExecuteReader();

            while (reader.Read())
            {

            }
            conn.CloseConnection();
            return null;
        }
    }
}
