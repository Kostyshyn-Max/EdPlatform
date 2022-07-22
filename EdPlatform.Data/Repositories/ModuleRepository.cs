using EdPlatform.Data.EF;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
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

        public void Create(Module entity)
        {
            _context.Modules.Add(entity);
        }

        public Module Get(int id)
        {
            return _context.Modules.Find(id);
        }

        public IEnumerable<Module> GetAll()
        {
            return _context.Modules;
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
