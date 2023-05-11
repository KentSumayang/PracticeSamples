namespace ActivityWeek9.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int Amount { get; set; }
        public string? Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
