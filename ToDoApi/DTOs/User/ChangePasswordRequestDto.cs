namespace ToDoApi.DTOs.User
{
    public class ChangePasswordRequestDto
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
