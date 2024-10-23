using Capstone.Application.Common.Paging;


namespace Capstone.Application.Common.Helpers
{
    public static class PaginationHelper
    {
        /// <summary>
        /// Applies pagination to an enumerable list of items.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="searchRequest">The paging query containing pagination parameters.</param>
        /// <param name="items">The list of items to paginate.</param>
        /// <returns>A paginated list of items.</returns>
        public static IEnumerable<T> PagingList<T>(PagingQuery searchRequest, IEnumerable<T> items)
        {
            return items.Skip(searchRequest.PageOffset)
                        .Take(searchRequest.PageSize);
        }
    }
}
