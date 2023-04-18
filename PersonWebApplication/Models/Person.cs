namespace PersonWebApplication.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public static int Count = 0;
        
        public Person(int id, string name, int age, string gender, string address) 
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
            this.Address = address;
        }
        
        

    }
}
