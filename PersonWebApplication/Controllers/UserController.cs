using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using PersonWebApplication.Models;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace PersonWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();

        [HttpGet("GetInformation")]
        public async Task<ActionResult<List<User>>> GetInformation()
        {
            string query = "SELECT * FROM user";
            Connection conn = new Connection();

            MySqlCommand command = new MySqlCommand(query, conn.GetConnection());
            MySqlDataReader reader;

            conn.OpenConnection();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                User u = new User();
                u.Username = reader.GetString("username");
                u.Password = reader.GetString("password");
                u.First_name = reader.GetString("first_name");
                u.Middle_name = reader.GetString("middle_name");
                u.Last_name = reader.GetString("last_name");
                u.Age = reader.GetInt32("age");
                u.Gender = reader.GetString("gender");
                u.Birthdate = reader.GetDateTime("birthdate");
                u.Nationality = reader.GetString("nationality");
                u.Country = reader.GetString("country");
                u.City = reader.GetString("city");
                u.Street = reader.GetString("street");
                users.Add(u);
            }
            conn.CloseConnection();
            return users;

        }
        [HttpPost("AddInformation")]

        public async Task<ActionResult<List<User>>> AddInformation(string username, string password, string first_name, string middle_name, string last_name, int age, string gender, string birthdate, string nationality, string country, string city, string street)
        {


            string query = "INSERT INTO user (username,password,first_name,middle_name,last_name,age,gender,birthdate,nationality,country,city,street)" +
                            "values('" + username + "','" + password + "','" + first_name + "','" + middle_name + "','" + last_name + "','" + age + "','" + gender + "','" + birthdate + "','" + nationality + "','" + country + "','" + city + "','" + street + "')";
            Connection conn = new Connection();

            MySqlCommand command = new MySqlCommand(query, conn.GetConnection());
            MySqlDataReader reader;

            conn.OpenConnection();

            reader = command.ExecuteReader();

            while (reader.Read())
            {

            }
            conn.CloseConnection();

            return Ok("New Data Added.");
        }
        [HttpPut("UpdateInformation")]

        public async Task<ActionResult<List<User>>> UpdateInformation(User user)
        {
            string query = "SELECT * FROM user WHERE username='" + user.Username + "' && password='" + user.Password + "'";
            Connection conn = new Connection();

            MySqlCommand command = new MySqlCommand(query, conn.GetConnection());
            MySqlDataReader reader;

            conn.OpenConnection();

            reader = command.ExecuteReader();

            if (reader.Read())
            {
                conn.CloseConnection();

                string updateQuery = "UPDATE user set username='" + user.Username + "',password='" + user.Password + "'" + "," +
                "first_name='" + user.First_name + "',middle_name='" + user.Middle_name + "',last_name='" + user.Last_name + "'," +
                "age='" + user.Age + "',gender='" + user.Gender + "',birthdate='" + user.Birthdate + "',nationality='" + user.Nationality + "'," +
                "country='" + user.Country + "',city='" + user.City + "',street='" + user.Street + "' WHERE username='" + user.Username + "'&& password='" + user.Password + "'";


                MySqlCommand updateCommand = new MySqlCommand(updateQuery, conn.GetConnection());
                MySqlDataReader updateReader;

                conn.OpenConnection();

                updateReader = updateCommand.ExecuteReader();
                conn.CloseConnection();

                return Ok("New Data Updated.");
            }
            else
            {
                return users;
            }



        }
        [HttpDelete("DeleteInformation")]
        public async Task<ActionResult<List<User>>> DeleteInformation(User user)
        {
            string query = "SELECT * FROM user WHERE username='" + user.Username + "' && password='" + user.Password + "'";
            Connection conn = new Connection();

            MySqlCommand command = new MySqlCommand(query, conn.GetConnection());
            MySqlDataReader reader;

            conn.OpenConnection();

            reader = command.ExecuteReader();

            if (reader.Read())
            {
                conn.CloseConnection();

                string deleteQuery = "DELETE FROM user " +
                                     "WHERE username='" + user.Username + "'&& password='" + user.Password + "'";


                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, conn.GetConnection());
                MySqlDataReader deleteReader;

                conn.OpenConnection();

                deleteReader = deleteCommand.ExecuteReader();

                conn.CloseConnection();

                return Ok("New Data Deleted.");
                
            }
            else
            {
                return users;
            }
        }
    }
}
