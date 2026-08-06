using System.Security.Claims;
using ToDoApi.Models;

namespace ToDoApi.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        return int.Parse(id);
    }

    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Name);
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole(UserRole.Admin.ToString());
    }
}