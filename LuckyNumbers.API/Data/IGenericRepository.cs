using System.Threading.Tasks;

namespace LuckyNumbers.API.Data
{
    public interface IGenericRepository
    {
         void add<T>(T entity) where T: class;
         void delete<T>(T entity) where T: class;
         Task<bool> saveAll();
    }
}