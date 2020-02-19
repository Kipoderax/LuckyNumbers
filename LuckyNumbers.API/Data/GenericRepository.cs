using System.Threading.Tasks;

namespace LuckyNumbers.API.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataContext context;
        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        public void add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

        public void delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public async Task<bool> saveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}