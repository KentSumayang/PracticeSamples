using System.ComponentModel.DataAnnotations;

namespace ActivityWeek9.Models
{
    public class Notification
    {
        [Key] public int Id { get; set; }
        public User User { get; set; }
        public Transaction Transaction { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
