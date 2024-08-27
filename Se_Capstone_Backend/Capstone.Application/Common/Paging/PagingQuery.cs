using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Application.Common.Paging
{
    public class PagingQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingQuery"/> class with default values.
        /// </summary>
        public PagingQuery()
        {
            PageIndex = 1;
            PageSize = 10;
            OrderBy = string.Empty;
            OrderByDesc = "asc";
        }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        public string SearchTerm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page size must be a positive number.")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page index must be a positive number.")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the order by field.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the order direction (asc/desc).
        /// </summary>
        public string OrderByDesc { get; set; }

        /// <summary>
        /// Gets the offset for pagination.
        /// </summary>
        public int PageOffset => (PageIndex - 1) * PageSize;

        /// <summary>
        /// Gets a dictionary of field mappings for sorting.
        /// </summary>
        /// <returns>A dictionary where the key is the field name and the value is the sort order.</returns>
        public virtual Dictionary<string, string> GetFieldMapping()
        {
            return new Dictionary<string, string>();
        }
    }
}
