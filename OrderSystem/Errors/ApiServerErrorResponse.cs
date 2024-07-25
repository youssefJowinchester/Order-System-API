namespace OrderSystem.Errors
{
    /// <summary>
    /// Represents a server error response in the API.
    /// </summary>
    public class ApiServerErrorResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets additional details about the server error.
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiServerErrorResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="message">An optional custom message.</param>
        /// <param name="details">Additional details about the error.</param>
        public ApiServerErrorResponse(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
