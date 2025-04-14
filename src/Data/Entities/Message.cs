namespace Data.Entities
{
    public class Message
    {
        public string Content { get; set; } = string.Empty;
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}