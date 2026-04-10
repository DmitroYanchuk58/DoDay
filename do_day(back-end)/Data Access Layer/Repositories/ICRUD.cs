using Data_Access_Layer.Entities;
using Task = System.Threading.Tasks.Task;

namespace Data_Access_Layer.Repositories
{
    public interface ICRUD <T> where T : Entity
    {
        public Task CreateAsync(T entity);

        // READ (Один за ID)
        public Task<T> GetByIdAsync(Guid id);

        // READ (Всі записи)
        public Task<List<T>> GetAllAsync();

        // UPDATE
        public Task UpdateAsync(T entity);

        // DELETE
        public Task DeleteAsync(Guid id);
    }
}
