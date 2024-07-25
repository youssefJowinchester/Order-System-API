namespace OrderSystem.Errors
{
    /// <summary>
    /// Represents a validation error response in the API.
    /// </summary>
    public class ApiValidationErrorResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the collection of validation error messages.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiValidationErrorResponse"/> class.
        /// </summary>
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
