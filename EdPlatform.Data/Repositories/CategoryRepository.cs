using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public async Task<Category?> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories.Where(expression).ToListAsync();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Categories.Find(id);
            if (entity != null)
                _context.Categories.Remove(entity);
        }
    }
}
