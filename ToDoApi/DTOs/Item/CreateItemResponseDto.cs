namespace ToDoApi.Models
{
    public class CreateItemResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}