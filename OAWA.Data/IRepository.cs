using System.Collections.Generic;
using System.Threading.Tasks;
using OAWA.Data.Helpers;
using OAWA.Data.Models;

namespace OAWA.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         
    }
}