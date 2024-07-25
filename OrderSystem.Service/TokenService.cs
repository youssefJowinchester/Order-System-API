using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderSystem.Core.Models;
using OrderSystem.Core.Repositories.Interfaces;
using OrderSystem.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Service
{
    /// <summary>
    /// Service to handle token creation
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a token asynchronously.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the token string.</returns>
        public Task<string> CreateTokenAsync(User user)
        {
            #region Payload Private Claims (Name, id, email,........)

            var AuthClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            #endregion

            #region Signature (1. Key)

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            #endregion

            #region Create Token

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            #endregion

            return Task.FromResult(tokenString);
        }
    }

}
