namespace ToDoApi.Models
{
    public class CreateItemRequestDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}