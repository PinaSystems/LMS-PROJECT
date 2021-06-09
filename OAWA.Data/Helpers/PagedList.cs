using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OAWA.Data.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.AddRange(items);
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
        }

        public async static Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);

        }

        public async static Task<PagedList<T>> CreateAsyncWithDtos(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count=0;
            var items = new  List<T>();
            await Task.Run(()=>{
                count = source.ToList().Count();
                items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            });    
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}