namespace ToDoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole? Role { get; set; } = UserRole.User;
        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}
