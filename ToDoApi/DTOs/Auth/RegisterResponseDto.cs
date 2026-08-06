using ToDoApi.Models;

namespace ToDoApi.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime Created_At { get; set; }
    }
}
