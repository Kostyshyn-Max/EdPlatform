using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.Repositories
{
    public class ModuleRepository : IRepository<Module>
    {
        private readonly ApplicationDbContext _context;
        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Module entity)
        {
            await _context.Modules.AddAsync(entity);
        }

        public async Task<Module> Get(int id)
        {
            return await _context.Modules.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAll()
        {
            return  await _context.Modules.ToListAsync();
        }

        public void Update(Module entity)
        {
            _context.Modules.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Modules.Find(id);
            if (entity != null)
                _context.Modules.Remove(entity);
        }
    }
}
