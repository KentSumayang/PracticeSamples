namespace PersonWebApplication.Models
{
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sample(int id, string name) 
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
