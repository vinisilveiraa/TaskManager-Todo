using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Services;

public class PasswordHashService
{
    private const int WorkFactor = 13;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}