using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Models;
using OrderSystem.Core.Services.Interfaces;
using OrderSystem.DTO;
using OrderSystem.Errors;
using OrderSystem.Service;

namespace OrderSystem.Controllers
{
    /// <summary>
    /// API controller for managing user authentication.
    /// </summary>
    public class UsersController : BaseApiController
    {
        private readonly IAuthService _userService;
        private readonly IMapper _mapper;

        public UsersController(IAuthService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #region Register
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userDto">User details</param>
        /// <returns>Status of the operation</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = userDto.Password; // Use Password property to store raw password temporarily

            var token = await _userService.RegisterAsync(user);
            return Ok(new { Token = token });
        }
        #endregion


        #region Login
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="loginDto">Login details</param>
        /// <returns>JWT token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
            return Ok(new { Token = token });
        }
        #endregion
    }

}
