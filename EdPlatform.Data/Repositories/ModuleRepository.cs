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
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;
        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Module entity)
        {
            await _context.Modules.AddAsync(entity);
        }

        public async Task<Module?> Get(int id)
        {
            return await _context.Modules.Where(x => x.ModuleId == id).Include(x => x.Lessons).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Module>> GetAll()
        {
            return  await _context.Modules.Include(x => x.Lessons).ToListAsync();
        }

        public async Task<IEnumerable<Module>> Find(Expression<Func<Module, bool>> expression)
        {
            return await _context.Modules.Where(expression).Include(x => x.Lessons).ToListAsync();
        }

        public void Update(Module entity)
        {
            _context.Modules.Update(entity);
        }

        public void Remove(int id)
        {
            var entity = _context.Modules.Find(id);
            if (entity != null)
                _context.Modules.Remove(entity);
        }
    }
}
