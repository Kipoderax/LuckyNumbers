using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data
{
    public interface IGenericRepository
    {
         void add<T>(T entity) where T: class;
         void delete<T>(T entity) where T: class;
         void update<T>(T entity) where T: class;
         Task<bool> saveAll();
    }
}