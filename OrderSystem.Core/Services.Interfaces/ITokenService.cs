using Microsoft.AspNetCore.Identity;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods for creating JWT tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user asynchronously.
        /// </summary>
        /// <param name="user">The user for whom to create the token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated token.</returns>
        Task<string> CreateTokenAsync(User user);
    }

}
