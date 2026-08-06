using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApi.Data;
using ToDoApi.DTOs.Auth;
using ToDoApi.Models;
using ToDoApi.Repositories;

namespace ToDoApi.Services
{
    public class JwtService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration; // iconfiguration acessa appsettings
        private readonly PasswordHashService _passwordService;
        private readonly RefreshTokenRepository _refreshTokenRepository;

        public JwtService(AppDbContext context,
            IConfiguration configuration,
            PasswordHashService passwordService,
            RefreshTokenRepository refreshTokenRepository)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseDto?> Authenticate(LoginRequestDto request)
        {
            // checa se veio nulo ou vazio
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            // checa se o usuario existe e se a senha coincide
            var userAccount = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (userAccount is null || !_passwordService.VerifyPassword(request.Password, userAccount.Password!))
                return null;

            return await GenerateJwtToken(userAccount);
        }

        private async Task<LoginResponseDto?> GenerateJwtToken(User user)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new JwtSecurityToken(issuer, audience,
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Role, user.Role.ToString()!)
                ],
                expires: tokenExpiryTimeStamp,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new LoginResponseDto
            {
                UserName = user.UserName,
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = await GenerateRefreshToken(user.Id)
            };
        }
        public async Task<string> GenerateRefreshToken(int userId)
        {
            var refreshTokenValidityMins = _configuration.GetValue<int>("JwtConfig:RefreshTokenValidityMins");
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiry = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
                UserId = userId
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken.Token!;
        }
        public async Task<LoginResponseDto?> ValidateRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetTokenAsync(token);
            if (refreshToken is null || refreshToken.Expiry < DateTime.UtcNow)
                return null;
            var user = await _context.Users.FindAsync(refreshToken.UserId);
            if (user is null)
                return null;

            await _refreshTokenRepository.DeleteAsync(refreshToken.Token);
            return await GenerateJwtToken(user);
        }
    }
}
