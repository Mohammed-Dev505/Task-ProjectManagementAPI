using Microsoft.EntityFrameworkCore;
using Task_ProjectManagementAPI.Data.Models;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int pageNumber, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResult<T>
            {
                Data = data,
                PageNumber = pageNumber,
                TotalCount = totalCount,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
