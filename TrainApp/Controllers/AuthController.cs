using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainApp.DTOs;
using TrainApp.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TrainApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
    
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registration a new user
        /// </summary>

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            if (!result)
                return BadRequest("A user with that name already exists.");
            return Ok("Registration successful");
        }

        /// <summary>
        /// Login and JWT token retrieval
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginUserAsync(dto);
            if ( token == null)
                return Unauthorized("Incorrect login or password");
            return Ok(new { token });
        }


        ///<summary>
        ///Get curret authenticated user info
        /// </summary>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null)
                return Unauthorized();

            if (!int.TryParse(userIdStr, out var userId))
                return BadRequest("Invalid user ID");

            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(new
            {
                username = user.UserName,
                email = user.Email,
            });
        }
    }
}
