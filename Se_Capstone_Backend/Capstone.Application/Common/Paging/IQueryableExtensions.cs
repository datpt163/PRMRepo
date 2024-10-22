using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Capstone.Application.Common.Paging
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByAndPaginate<T>(
            this IQueryable<T> query,
            dynamic request)
        {
            string? orderBy = request.OrderBy as string;
            string? orderByDesc = request.OrderByDesc as string;

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            if (!string.IsNullOrEmpty(orderByDesc))
            {
                query = query.OrderBy($"{orderByDesc} descending");
            }

            int pageIndex = (int)request.PageIndex;
            int pageSize = (int)request.PageSize;

            if (pageIndex > 0 && pageSize > 0)
            {
                int skip = (pageIndex - 1) * pageSize;
                query = query.Skip(skip).Take(pageSize);
            }

            return query;
        }
    }
}
