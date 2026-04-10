using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repositories
{
    public interface IJoin
    {
        public Task<Category> GetCategoryWithOptionsAsync(Guid id);
    }
}
