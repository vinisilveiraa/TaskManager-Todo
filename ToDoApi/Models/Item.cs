namespace ToDoApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string PublicCode { get; set; } = string.Empty;
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Completed_At { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}