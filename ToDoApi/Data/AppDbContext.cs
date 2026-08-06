using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Data
{
    public class AppDbContext : DbContext
    {
        private readonly PasswordHashService _passwordHashService;

        public AppDbContext(DbContextOptions<AppDbContext> options, PasswordHashService passwordHashService) : base(options)
        {
            _passwordHashService = passwordHashService;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    Password = _passwordHashService.HashPassword("admin"),
                    Role = UserRole.Admin,
                });
        }
    }
}
