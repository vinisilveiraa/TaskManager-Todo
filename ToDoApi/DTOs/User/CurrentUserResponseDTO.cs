using ToDoApi.Models;

namespace ToDoApi.DTOs.User
{
    public class CurrentUserResponseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole? Role { get; set; } = UserRole.User;
        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}
