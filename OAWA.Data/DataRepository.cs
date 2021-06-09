using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OAWA.Data.Helpers;
using OAWA.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace OAWA.Data
{
    public abstract class DataRepository : IRepository
    {
        protected readonly DataContext _context;
        public DataRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

       
    }
}