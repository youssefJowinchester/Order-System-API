namespace OrderSystem.Helpers
{
    /// <summary>
    /// Represents a paginated result set.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in the paginated result.</typeparam>
    public class Pagination<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pagination{T}"/> class.
        /// </summary>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="pageIndex">The current page index (1-based).</param>
        /// <param name="count">The total number of items available.</param>
        /// <param name="data">The items for the current page.</param>
        public Pagination(int pageSize, int pageIndex, int count, IEnumerable<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Data = data;
        }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the current page index (1-based).
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the total number of items available.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets the items for the current page.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
