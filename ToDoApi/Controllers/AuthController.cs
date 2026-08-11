using Microsoft.AspNetCore.Mvc;
using ToDoApi.Repositories;
using ToDoApi.Models;
using ToDoApi.Services;
using Microsoft.AspNetCore.Authorization;
using ToDoApi.DTOs.Auth;
using ToDoApi.Helpers;

namespace ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly PasswordHashService _passwordService;
        private readonly UserRepository _userRepository;
        private readonly RefreshTokenRepository _refreshTokenRepository;
        public AuthController(
            JwtService jwtService,
            UserRepository userRepository,
            PasswordHashService passwordService,
            RefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _passwordService = passwordService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result is null)
                return Unauthorized(new { message = "Invalid credentials" });
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto user)
        {
            if (user == null)
                return BadRequest("Item is null.");

            var existingUser = await _userRepository.GetByUserNameAsync(user.UserName);
            if (existingUser != null)
                return BadRequest("User already exists.");

            var result = new User
            {
                UserName = user.UserName,
                Password = _passwordService.HashPassword(user.Password),
                Role = UserRole.User,
                Created_At = DateTime.UtcNow
            };
            var response = new RegisterResponseDto
            {
                Id = result.Id,
                UserName = result.UserName,
                Role = UserRole.User,
                Created_At = result.Created_At
            };

            await _userRepository.AddAsync(result);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshRequestDto request)
        {
            if (string.IsNullOrEmpty(request.refreshToken))
                return BadRequest("Invalid Token");

            var result = await _jwtService.ValidateRefreshToken(request.refreshToken);
            if (result is null)
                return Unauthorized(new { message = "Invalid refresh token" });

            return Ok(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetUserId();
            await _refreshTokenRepository.DeleteByUserIdAsync(userId);
            return NoContent();
        }
    }
}
