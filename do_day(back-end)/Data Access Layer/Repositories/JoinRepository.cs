using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class JoinRepository
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
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
