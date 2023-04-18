using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class User
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private string First_name { get; set; }
        private string Middle_name { get; set; }
        private string Last_name { get; set; }
        private int Age { get; set; }
        private DateTime Birthdate { get; set; }
        private string Nationality { get; set; }
        private string Country { get; set; }
        private string City { get; set; }
        private string Street { get; set; }
        public User(string username, string password, string first_name, string middle_name, string last_name, int age, DateTime birthdate, string nationality, string country, string city, string street) 
        {
            this.Username = username;
            this.Password = password;
            this.First_name = first_name;
            this.Middle_name = middle_name;
            this.Last_name = last_name;
            this.Age = age;
            this.Birthdate = birthdate;
            this.Nationality = nationality;
            this.Country = country;
            this.City = city;
            this.Street = street;
        }
    }
}