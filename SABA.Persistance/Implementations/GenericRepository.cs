using Microsoft.EntityFrameworkCore;
using SABA.Core.Abstractions;
using SABA.Core.Models.ProductModel;
using SABA.Persistance.DataContext;
using System.Linq.Expressions;

namespace PashaBank.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _databaseContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task Add(T entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _databaseContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _databaseContext.Set<Product>().Include(p => p.Images).ToListAsync();
            }

            return await _databaseContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _databaseContext.Set<Product>()
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.ProductId == id) as T;
            }

            return await _databaseContext.Set<T>().FindAsync(id);
        }


        public void Update(T entity)
        {
            _databaseContext.Update(entity);
        }
    }
}
