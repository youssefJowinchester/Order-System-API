using Microsoft.Extensions.Configuration;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user and returns a token.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <returns>A token for the registered user.</returns>
        /// <exception cref="ArgumentException">Thrown when the password is null or empty.</exception>
        public async Task<string> RegisterAsync(User user)
        {
            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(user.PasswordHash));
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return await _tokenService.CreateTokenAsync(user);
        }

        /// <summary>
        /// Logs in a user and returns a token.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A token for the logged-in user.</returns>
        /// <exception cref="Exception">Thrown when the credentials are invalid.</exception>
        public async Task<string> LoginAsync(string username, string password)
        {
            var user = (await _unitOfWork.Repository<User>().GetAllAsync()).FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }

            return await _tokenService.CreateTokenAsync(user);
        }
    }

}
