using ToDoApi.Models;

namespace ToDoApi.DTOs.User
{
    public class UpdateUserResponseDto
    {
        public string UserName { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole? Role { get; set; } = UserRole.User;
    }
}
