using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositories
{
    public class JoinRepository : IJoin
    {
        private readonly DoDayDBContext _context;

        public JoinRepository(DoDayDBContext context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryWithOptionsAsync(Guid id)
        {
            return await _context.Categories
                .Include(c => c.CategoryOptions)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllUserCategoriesWithOptionsAsync(Guid idUser)
        {
            return await _context.Categories
                .Include(c => c.CategoryOptions)
                .Where(c => c.IdUser == idUser)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
