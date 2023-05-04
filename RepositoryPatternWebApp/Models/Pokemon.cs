using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternWebApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
