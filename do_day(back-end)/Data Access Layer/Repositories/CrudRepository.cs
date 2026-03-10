using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Task = System.Threading.Tasks.Task;

namespace Data_Access_Layer.Repositories
{
    public class CrudRepository<T> : ICRUD<T> where T : Entity
    {
        private readonly DoDayDBContext _context;
        private readonly DbSet<T> _dbSet;

        public CrudRepository(DoDayDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // CREATE
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // READ (Один за ID)
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // READ (Всі записи)
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // UPDATE
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}