using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods for user authentication and registration.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the registration status or token.</returns>
        Task<string> RegisterAsync(User user);

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a token if successful.</returns>
        Task<string> LoginAsync(string username, string password);
    }

}
