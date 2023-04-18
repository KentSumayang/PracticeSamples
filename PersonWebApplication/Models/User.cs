namespace PersonWebApplication.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public User(string username, string password, string first_name, string middle_name, string last_name, int age, string gender, DateTime birthdate, string nationality, string country, string city, string street)
        {
            Username = username;
            Password = password;
            First_name = first_name;
            Middle_name = middle_name;
            Last_name = last_name;
            Age = age;
            Gender = gender;
            Birthdate = birthdate;
            Nationality = nationality;
            Country = country;
            City = city;
            Street = street;
        }

        public User()
        {

        }
    }
}
