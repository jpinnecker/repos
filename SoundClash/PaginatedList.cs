using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash
{
    /// <summary>
    /// Paginated list example from microsoft docs.
    /// Creates shortened list from database with the ability to switch between pages.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        private PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int totalPages)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
            

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = await source.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Validating pageIndex
            if (pageIndex > totalPages || pageIndex < 1)
                throw new ArgumentException("Invalid Index!");

            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, totalPages);
        }
    }
}
