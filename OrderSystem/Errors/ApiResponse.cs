namespace OrderSystem.Errors
{
    /// <summary>
    /// Represents the standard API response structure.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="message">An optional custom message.</param>
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Gets the default message for the specified HTTP status code.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <returns>The default message.</returns>
        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request has been made.",
                401 => "You are not authorized.",
                404 => "Resource was not found.",
                500 => "Internal server error.",
                _ => null
            };
        }
    }

}
