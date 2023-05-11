using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityWeek9.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public decimal Balance { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

    }
}
